const modal = document.getElementById('modal');

// Изменяем стиль modal сразу при загрузке страницы
window.onload = function() {
 modal.style.display = 'flex';
};

const addTaskButton = document.getElementById('add-task');

addTaskButton.addEventListener('click', () => {
    window.open('\\TasksBoard/task_index.html', '_blank');
 // Здесь вы можете добавить логику обработки данных из формы
 // Например, сохранить данные в базу данных или вывести их в консоль
 console.log('Название:', document.getElementById('task-name').value);
 console.log('Описание:', document.getElementById('task-description').value);
 console.log('Срок:', document.getElementById('task-deadline').value);
 console.log('Ответственный:', document.getElementById('task-responsible').value);
 // После обработки данных можно закрыть окно
 modal.style.display = 'none';
});