// worker.js
importScripts('https://cdn.jsdelivr.net/npm/xlsx@0.17.4/dist/xlsx.full.min.js');

let data = []; //Se almacena los dataos
let totalForRegister = 0; //Se almacena el total de los registro para registrar
let fileCount = 0; //Conteo de los archivo leidos
let programsCount = 1;

let corructedFiles = [];

try {
    onmessage = async (e) => {
        if (e.data.type === "read-files") {
            data = [];
            totalForRegister = 0;
            fileCount = 0;
            let files = e.data.value;
            let file = null;
            for (file of files) {
                totalForRegister++;
            }
            for (const file of files) {
                const reader = new FileReader();
                const promise = new Promise((resolve, reject) => {
                    reader.onload = async (e) => {
                        try {
                            const workbook = XLSX.read(e.target.result, { type: "binary" });
                            const sheetName = workbook.SheetNames[0];
                            const worksheet = workbook.Sheets[sheetName];
                            data = data.concat(XLSX.utils.sheet_to_json(worksheet));
                            let tempData = data.concat(XLSX.utils.sheet_to_json(worksheet));
                            let columns = ["PRF_CODIGO", "PRF_VERSION", "PRF_DENOMINACION", "NIVEL DE FORMACION","PRF_DURACION_MAXIMA"];
                            console.log(tempData[0]);
                            if (!tempData[0].hasOwnProperty(columns)) corructedFiles.push(file.name);
                            fileCount++;
                            postMessage({
                                type: "loading_read_files",
                                value: {
                                    porcentage: calculatePorcentage(fileCount, totalForRegister),
                                    textValue: `${fileCount} de ${totalForRegister} ${totalForRegister === 1 ? "archivo" : "archivos"}`
                                }
                            });
                            resolve();
                        } catch (error) {
                            reject(error);
                        }
                    };
                    reader.readAsArrayBuffer(file);
                });
                await promise;
            }
            if (totalForRegister === fileCount) {
                postMessage({ type: "ok_read_files" });
                console.log(corructedFiles)
            }
        }
        if (e.data.type === "register") {
            programsCount = 1;

            for (var program of data) {
                const res = await fetch("https://localhost:44309/Programa_Formacion/programRegister", {
                    method: "POST",
                    headers: { "Content-type": "application/json" },
                    body: JSON.stringify({
                        NombreArea:program["Red Tecnológica"],
                        NombreRed:program["Red de Conocimiento"],
                        DenominacionPrograma: program["PRF_DENOMINACION"],
                        VersionPrograma: program["PRF_VERSION"],
                        NivelPrograma: program["NIVEL DE FORMACION"],
                        CodigoPrograma: program["PRF_CODIGO"],
                        HorasPrograma: program["PRF_DURACION_MAXIMA"],
                        EstadoPrograma: ""
                    })
                })
                res.json().then(json => {
                    console.log(json);
                    postMessage({
                        type: "userUpload",
                        value: {
                            porcentage: calculatePorcentage(programsCount, data.length)
                        }
    
                    });
                    programsCount++;
                })

                /* res.then(json=>{
                    
                }) */
            }
        }
    }

} catch (e) {
    console.log(e)
}


//Se calcular el porcentage el progreso 
function calculatePorcentage(number, maxLenght) {
    return Math.trunc((number / maxLenght) * 100)
}