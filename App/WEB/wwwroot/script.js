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
    window.open('\\TasksBoard/task_index.html', '_blank'); 
  } else {
    // Выводим сообщение об ошибке, если поля не заполнены
    alert('Пожалуйста, заполните все поля!');
  }
});

registerButton.addEventListener('click', () => {
  // Проверяем, заполнены ли поля формы
  const login = document.getElementById('login').value;
  const password = document.getElementById('password').value;
    window.open('\\Registration/reg_index.html', '_blank'); 
  }
);