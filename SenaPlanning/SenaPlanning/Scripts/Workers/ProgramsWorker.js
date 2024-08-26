// worker.js
importScripts('https://cdn.jsdelivr.net/npm/xlsx@0.17.4/dist/xlsx.full.min.js');

let data = []; //Se almacena los dataos
let totalForRegister = 0; //Se almacena el total de los registro para registrar
let count = 0; //Conste de los registrados
let totalData = 0;

let corructedFiles = [];

try {
    onmessage = async(e) => {
        if (e.data.type === "read-files") {
            data = [];
            totalForRegister = 0;
            count = 0;
            let files = e.data.value;
            let file = null;
            for (file of files) {
                totalForRegister++;
            }
            for (const file of files) {
                const reader = new FileReader();
                const promise = new Promise((resolve, reject) => {
                    reader.onload = async(e) => {
                        try {
                            const workbook = XLSX.read(e.target.result, { type: "binary" });
                            const sheetName = workbook.SheetNames[0];
                            const worksheet = workbook.Sheets[sheetName];
                            data = data.concat(XLSX.utils.sheet_to_json(worksheet));
                            let tempData = data.concat(XLSX.utils.sheet_to_json(worksheet));
                            let columns = ["PRF_CODIGO", "PRF_VERSION", "PRF_DENOMINACION", "NIVEL DE FORMACION", true];
                            if (!tempData[0].hasOwnProperty(columns)) corructedFiles.push(file.name);
                            count++;
                            postMessage({
                                type: "loading_read_files",
                                value: {
                                    porcentage: calculatePorcentage(count, totalForRegister),
                                    textValue: `${count} de ${totalForRegister} ${totalForRegister === 1 ? "archivo" : "archivos"}`
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
            if (totalForRegister === count) {
                postMessage({ type: "ok_read_files" });
                console.log(corructedFiles)
            }
        }
        if (e.data.type === "register") {
            console.log(data)
            for (var element of data) {
                const res = await fetch("https://localhost:44309/Programa_Formacion/programRegister", {
                    method: "POST",
                    headers: { "Content-type": "application/json" },
                    body: JSON.stringify({
                        DenominacionPrograma: "",
                        VersionPrograma: "",
                        NivelPrograma: "",
                        CodigoPrograma: "",
                        HorasPrograma: "",
                        EstadoPrograma: ""
                    })
                })
                res.json().then(json => {
                    console.log(json);
                })

                /* res.then(json=>{
                    
                }) */
                postMessage({
                    type: "loading_read_files",
                    value: {
                        porcentage: calculatePorcentage(count, totalForRegister),
                        textValue: `${count} de ${totalForRegister} ${totalForRegister === 1 ? "archivo" : "archivos"}`
                    }
                });
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