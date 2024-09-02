const workerPrograms = new Worker("/Scripts/Workers/ProgramsWorker.js", {

});

let container_progress_bar_programs = document.getElementById("container-progress-bar_programs");
let text_progressBar_files_programs = document.getElementById("text-progressBar-files_programs");
let porcentage_progressBar_files_programs = document.getElementById("porcentage-progressBar-files_programs");
let bar_progressBar_files_programs = document.getElementById("bar-progressBar-files_programs");
let message_progressBar_files_programs = document.getElementById("message-progressBar-files_programs");
let buttom_progressBar_files_programs = document.getElementById("buttom-progressBar-files_programs");
let inputFile_programs = document.getElementById("inputFile-programs");

workerPrograms.onmessage = async (e) => {
    if (e.data.type == "loading_read_files") {
        container_progress_bar_programs.style.display = "";
        text_progressBar_files_programs.innerHTML = "Leyendo archivos...";
        porcentage_progressBar_files_programs.style.display = "";
        porcentage_progressBar_files_programs.innerHTML = `${e.data.value.textValue}`;
        bar_progressBar_files_programs.style.width = `${e.data.value.porcentage}%`;
        message_progressBar_files_programs.style.display = "none";
        buttom_progressBar_files_programs.style.display = "none";
        return
    }
    if (e.data.type === "ok_read_files") {
        porcentage_progressBar_files_programs.style.display = "none";
        buttom_progressBar_files_programs.style.display = "";
        text_progressBar_files_programs.innerHTML = "Lectura exitosa";
        return
    }
    if (e.data.type === "userUpload") {
        container_progress_bar_programs.style.display = "";
        text_progressBar_files_programs.innerHTML = "Registrando programas...";
        porcentage_progressBar_files_programs.style.display = "";
        porcentage_progressBar_files_programs.innerHTML = `${e.data.value.porcentage}%`;
        bar_progressBar_files_programs.style.width = `${e.data.value.porcentage}%`;
        message_progressBar_files_programs.style.display = "none";
        buttom_progressBar_files_programs.style.display = "none";
        return
    }
    if (e.data.type === "ok_register") {
        window.location.href ="/Programa_Formacion"
        return
    }
}

inputFile_programs.addEventListener('change', async (event) => {
    workerPrograms.postMessage({ type: "read-files", value: event.target.files });
    buttom_progressBar_files_programs.style.display = "none";
    message_progressBar_files_programs.style.display = "none";
    event.target.value = "";
})

buttom_progressBar_files_programs.addEventListener("click", () => {
    workerPrograms.postMessage({ type: "register" });
})