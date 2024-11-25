
function SortDiv(sid,flag) {
  const parent = document.getElementById(sid + "-s");
  const children = parent.children;
  const array = Array.prototype.slice.call(children);
  array.sort((a, b) => {
    const dateA = new Date(a.querySelector('input[type="date"]').value);
    const dateB = new Date(b.querySelector('input[type="date"]').value);
    if(flag)
    return dateB - dateA; // для сортировки по возрастанию
    else
    return dateA - dateB; // для сортировки по убыванию
  });

  parent.innerHTML = '';
  array.forEach(child => parent.appendChild(child));
}
function blockInputs(id) {
const flag =!document.getElementById(id + "-text").disabled;
const element = document.getElementById(id + "-bt-task");
if (!flag) {
    element.textContent = "✅";
}
if(flag)
{
    element.textContent = "📒";
}
document.getElementById(id + "-text").disabled = flag;
document.getElementById(id + "-description").disabled = flag;
document.getElementById(id + "-deadline").disabled = flag;
}
function RemoveDiv(id,sid) {
const parent = document.getElementById(sid+"-s")
const child = document.getElementById(id);
parent.removeChild(child);
}