//document.addEventListener('DOMContentLoaded', documentReady);
//function documentReady() {
//    console.log('Dom загружен')
//    let buttons = document.querySelectorAll('#courseSelector');
//    for (let i = 0; i < buttons.length; i++) {
//        buttons[i].addEventListener('click', startTest);
//    }
//};

//function startTest() {
//    let temp = this.parentNode;
//    let courseName = temp.getElementsByTagName('h2')[0].innerHTML;
//    console.log(courseName);

//    let toSend = { course: courseName }
//    console.log(toSend);

//    let xhr = new XMLHttpRequest();
//    xhr.open("POST", "/Home/StartTest"); // async=true
//    xhr.onload = function (e) {
//        if (xhr.readyState == 4 && xhr.status == 200) {
//            //console.log(xhr.responseText);
//        }
//    };
//    xhr.send(toSend);
//}