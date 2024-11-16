function blockInputs() {
    document.getElementById("login").disabled = true;
    document.getElementById("password").disabled = true;
    document.getElementById("repeat-password").disabled = true;
    setTimeout(function () {
        document.getElementById("login").disabled = false;
        document.getElementById("password").disabled = false;
        document.getElementById("repeat-password").disabled = false;
    }, 5000);
}