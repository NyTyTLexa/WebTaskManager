const loginButton = document.querySelector('.login-button');
const registerButton = document.querySelector('.register-button');

loginButton.addEventListener('click', () => {
  // Проверяем, заполнены ли поля формы
  const login = document.getElementById('login').value;
  const password = document.getElementById('password').value;

  if (login && password) {
    // Здесь вы должны добавить свою логику проверки
    // Например, отправку данных на сервер для аутентификации

    // Вместо этого просто откроем новое окно
    alert('Вы успешно зарегестрировались в системе!');
    window.open('\\TasksBoard/task_index.html', '_blank'); 
  } else {
    // Выводим сообщение об ошибке, если поля не заполнены
    alert('Пожалуйста, заполните все поля!');
  }
});

<style>
    <style>
        body {
            margin: 0;
        padding: 0;
        font-family: 'Arial', sans-serif;
        background: linear-gradient(to bottom, #6284ff, #8366ff);
        color: #333;
        display: flex;
        justify-content: center;
        align-items: center;
        min-height: 100vh;
    }

        .container {
            background - color: white;
        padding: 30px;
        border-radius: 10px;
        box-shadow: 0px 0px 20px rgba(0, 0, 0, 0.1);
        width: 350px;
        text-align: center;
    }

        .login-form {
            margin - top: 30px;
    }

        .home-icon .circle {
            width: 40px;
        height: 40px;
        border-radius: 50%;
        background-color: white;
        overflow: hidden;
        display: flex;
        justify-content: center;
        align-items: center;
    }

        .home-icon img {
            width: 50%;
        height: auto;
    }

        h1 {
            font - size: 24px;
        margin-bottom: 30px;
        font-weight: bold;
    }

        .form-group {
            margin - bottom: 20px;
    }

        label {
            display: block;
        margin-bottom: 5px;
        font-weight: bold;
    }

        input {
            width: 100%;
        padding: 10px;
        border: 1px solid #ddd;
        border-radius: 5px;
        box-sizing: border-box;
        font-size: 16px;
    }

        .login-button, .register-button {
            background - color: #5479ff;
        color: white;
        padding: 12px 20px;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        font-size: 18px;
        transition: background-color 0.3s ease;
        box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.1);
        margin-bottom: 10px;
    }

        .login-button:hover, .register-button:hover {
            background - color: #395dff;
        }

</style>