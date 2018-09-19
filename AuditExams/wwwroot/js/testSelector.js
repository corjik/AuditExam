//Вешаем обработчики на кнопки
document.addEventListener('DOMContentLoaded', documentReady);
function documentReady() {
    console.log('Dom загружен')
    let buttons = document.querySelectorAll('#courseSelector');
    for (let i = 0; i < buttons.length; i++) {
        buttons[i].addEventListener('click', startTest);
    };
};

//Переходим на страницу тестирования
function startTest() {
    let temp = this.parentNode;
    let courseName = temp.getElementsByTagName('h2')[0].innerHTML;
    let toSend = { course: courseName }

    let xhr = new XMLHttpRequest();
    xhr.open("GET", "/Home/Exam"); // async=true
    xhr.onload = function (e) {
        if (xhr.readyState === 4 && xhr.status === 200) {
            let href = '/Home/Exam/' + '?course=' + courseName;
            location.href = href
        };
    };
    xhr.send(toSend);
}