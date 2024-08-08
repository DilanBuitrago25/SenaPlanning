try {
    importScripts("https://cdn.jsdelivr.net/npm/xlsx@0.17.4/dist/xlsx.full.min.js");
} catch (e) {
    console.log(e);
}
let data = [];//Se almace los dataos
let totalForRegister = 0; //Se almacena el total de los registro para registrar
let count = 0; //Conste de los registrados
let totalData = 0;

try {
    onmessage = async (e) => {
        if (e.data.type === "read-files") {
            data = [];
            let files = e.data.value;
            let file = null;
            for (file of files) {
                totalForRegister++;
            }

            for (const file of files) {
                console.log(file);
                const reader = new FileReader();
                const promise = new Promise((resolve, reject) => {
                    reader.onload = async (e) => {
                        try {
                            const workbook = XLSX.read(e.target.result, { type: "binary" });
                            const sheetName = workbook.SheetNames[0];
                            const worksheet = workbook.Sheets[sheetName];
                            data = data.concat(XLSX.utils.sheet_to_json(worksheet));
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
                console.log(data)
            }
        }
    }
} catch (e) {

}


//Se calcula el porcentage el progreso 
function calculatePorcentage(number, maxLenght) {
    return Math.trunc((number / maxLenght) * 100)
}