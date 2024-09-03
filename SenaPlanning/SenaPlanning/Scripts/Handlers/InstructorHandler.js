const workerInstructor = new Worker("/Scripts/Workers/InstructorWorker.js", {

});

let container_progress_bar_Instructor = document.getElementById("container-progress-bar_Instructor");
let text_progressBar_files_Instructor = document.getElementById("text-progressBar-files_Instructor");
let porcentage_progressBar_files_Instructor = document.getElementById("porcentage-progressBar-files_Instructor");
let bar_progressBar_files_Instructor = document.getElementById("bar-progressBar-files_Instructor");
let message_progressBar_files_Instructor = document.getElementById("message-progressBar-files_Instructor");
let buttom_progressBar_files_Instructor = document.getElementById("buttom-progressBar-files_Instructor");
let inputFile_Instructor = document.getElementById("inputFile-Instructor");

workerInstructor.onmessage = async (e) => {
    if (e.data.type == "loading_read_files") {
        container_progress_bar_Instructor.style.display = "";
        text_progressBar_files_Instructor.innerHTML = "Leyendo archivos...";
        porcentage_progressBar_files_Instructor.style.display = "";
        porcentage_progressBar_files_Instructor.innerHTML = `${e.data.value.textValue}`;
        bar_progressBar_files_Instructor.style.width = `${e.data.value.porcentage}%`;
        message_progressBar_files_Instructor.style.display = "none";
        buttom_progressBar_files_Instructor.style.display = "none";
        return
    }
    if (e.data.type === "ok_read_files") {
        porcentage_progressBar_files_Instructor.style.display = "none";
        buttom_progressBar_files_Instructor.style.display = "";
        text_progressBar_files_Instructor.innerHTML = "Lectura exitosa";
        return
    }
    if (e.data.type === "userUpload") {
        container_progress_bar_Instructor.style.display = "";
        text_progressBar_files_Instructor.innerHTML = "Registrando programas...";
        porcentage_progressBar_files_Instructor.style.display = "";
        porcentage_progressBar_files_Instructor.innerHTML = `${e.data.value.porcentage}%`;
        bar_progressBar_files_Instructor.style.width = `${e.data.value.porcentage}%`;
        message_progressBar_files_Instructor.style.display = "none";
        buttom_progressBar_files_Instructor.style.display = "none";
        return
    }
    if (e.data.type === "ok_register") {
        window.location.href ="/Instructors"
        return
    }
}

inputFile_Instructor.addEventListener('change', async (event) => {
    workerInstructor.postMessage({ type: "read-files", value: event.target.files });
    buttom_progressBar_files_Instructor.style.display = "none";
    message_progressBar_files_Instructor.style.display = "none";
    event.target.value = "";
})

buttom_progressBar_files_Instructor.addEventListener("click", () => {
    workerInstructor.postMessage({ type: "register" });
})