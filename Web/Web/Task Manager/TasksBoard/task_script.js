const columns = document.querySelectorAll('.column');
const tasks = document.querySelectorAll('.task');
const addTaskButton = document.getElementById('add-task-button');

let draggedTask = null;

tasks.forEach(task => {
  task.addEventListener('dragstart', handleDragStart);
  task.addEventListener('dragover', handleDragOver);
  task.addEventListener('drop', handleDrop);

  // Добавление кнопок "Редактировать" и "Удалить"
  const editButton = document.createElement('button');
  editButton.textContent = 'Редактировать';
  editButton.classList.add('edit-task-button');

  const deleteButton = document.createElement('button');
  deleteButton.textContent = 'Удалить';
  deleteButton.classList.add('delete-task-button');

  const actionsDiv = document.createElement('div');
  actionsDiv.classList.add('actions');
  actionsDiv.appendChild(editButton);
  actionsDiv.appendChild(deleteButton);

  task.appendChild(actionsDiv);
});

columns.forEach(column => {
  column.addEventListener('dragover', handleDragOver);
  column.addEventListener('drop', handleDrop);
});

addTaskButton.addEventListener('click', () => {
  window.open('\\AddTask/add_task_index.html', '_blank'); 
  /*
  // Создаем новую задачу
  const newTask = document.createElement('div');
  newTask.classList.add('task');
  newTask.draggable = true;
  newTask.innerHTML = `
    <h3>Новая задача</h3>
    <p>Описание задачи.</p>
    <p>Срок: 2023-12-31</p>
    <div class="actions">
      <button class="edit-task-button">Редактировать</button>
      <button class="delete-task-button">Удалить</button>
    </div>
  `;
  */
  // Добавляем обработчики событий для новой задачи
  newTask.addEventListener('dragstart', handleDragStart);
  newTask.addEventListener('dragover', handleDragOver);
  newTask.addEventListener('drop', handleDrop);

  // Добавляем новую задачу в колонку "Новые"
  document.getElementById('todo').querySelector('.task-area').appendChild(newTask);
});

function handleDragStart(event) {
  draggedTask = event.target;
  event.dataTransfer.setData('text/plain', event.target.id);
  event.target.classList.add('dragging');
}

function handleDragOver(event) {
  event.preventDefault();
}

function handleDrop(event) {
  event.preventDefault();

  const targetColumn = event.target.closest('.column');
  if (targetColumn) {
    targetColumn.querySelector('.task-area').appendChild(draggedTask);
    draggedTask.classList.remove('dragging');
    draggedTask = null;
  }
}