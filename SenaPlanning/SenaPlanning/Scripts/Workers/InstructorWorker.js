// worker.js
importScripts('https://cdn.jsdelivr.net/npm/xlsx@0.17.4/dist/xlsx.full.min.js');

let data = []; //Se almacena los dataos
let totalForRegister = 0; //Se almacena el total de los registro para registrar
let fileCount = 0; //Conteo de los archivo leidos
let instructorsCount = 1;

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
                            let columns = ["PRF_CODIGO", "PRF_VERSION", "PRF_DENOMINACION", "NIVEL DE FORMACION", "PRF_DURACION_MAXIMA"];
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
                //console.log(corructedFiles)
            }
        }
        if (e.data.type === "register") {
            instructorsCount = 1;
            for (var instructor of data) {
                const res = await fetch("/Instructors/instructorRegister", {
                    method: "POST",
                    headers: { "Content-type": "application/json" },
                    body: JSON.stringify({
                        DocumentoInstructor: instructor["DOCUMENTO"],
                        NombreCompletoInstructor: instructor["NOMBRE_COMPLETO"].toUpperCase(),
                        CodRegionalInstructor: instructor["COD_REGIONAL"],
                        RegionalInstructor: instructor["REGIONAL"],
                        CodCCOS: instructor["COD_CCOS"],
                        DependenciaInstructor: instructor["DESPENDENCIA"],
                        CargoInstructor: instructor["CARGO"],
                        TipoCargoInstructor: instructor["TIPO DE CARGO"],
                        CorreoSENAInstructor: instructor["CORREO SENA"],
                        RedInstructor: instructor["RED"],
                        AreaInstructor: instructor["ÁREA"],
                        RutaInstructor: instructor["RUTA"],
                        CodMunicipioInstructor: instructor["COD MUNICIPIO"],
                        MunicipioInstructor: instructor["MUNICIPIO DEPENDENCIA"],
                        FechaNacimientoInstructor: instructor["FECHA_NACIMIENTO"],
                        FechaIngreso: instructor["FECHA_INGRESO"],
                        SexoInstructor: instructor["SEXO"],
                    })
                })
                res.json().then(json => {
                    postMessage({
                        type: "userUpload",
                        value: {
                            porcentage: calculatePorcentage(instructorsCount, data.length)
                        }
                    });
                    instructorsCount++;
                })
            }
            if (data.length === instructorsCount) {
                postMessage({ type: "ok_register" })
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