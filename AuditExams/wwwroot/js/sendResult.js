// Write your JavaScript code.

$('#sendBtn').click(function () {
    let tempData = {};
    let toSend = [];
    let forms = $('fieldset');
    forms.each(function () {
        tempData['QuestionId'] = $(this).find('#questionId').val();
        tempData['AnswerNum'] = $(this).find('input:checked').val();
        tempData['AnswerText'] = $(this).find('input:checked').parent().text();
        toSend.push(tempData);
        tempData = {};
    });

    $.ajax({
        type: "POST",
        url: "/Home/SendResult",
        data: {
            result: toSend
        },
        //contentType: 'application/json; charset=utf-8',
        //dataType: "json",
        success: successFunc,
        error: errorFunc
    });
});


function successFunc(data, status) {
    console.log('Все успешно');
    localStorage.clear();
    let toDel = document.querySelector('#questionForm');
    console.log(toDel);
    if (toDel.parentNode) {
        toDel.parentNode.removeChild(toDel);
    }

    loadResult(data);

};

function errorFunc(errorData) {
    console.log(errorData.responseText);
};


function loadResult(data) {
    let resultList = JSON.parse(data);
    let correctAnswers = 0;
    let failList = [];

    for (let i = 0; i < resultList.length; i++) {
        if (resultList[i].IsCorrect === true) {
            correctAnswers++
        }
        else {
            pushObj = {
                'Текст вопроса': resultList[i].Question.Text,
                'Ваш ответ': resultList[i].AnswerText,
            };
            failList.push(pushObj);
            console.log(failList)
        }
    }

    let parentEl = document.querySelector('div.body-content');
    parentEl.innerHTML = '';
    parentEl.innerHTML = '<h2> Результаты тестирования:</h2> \
        < div > Правильно отвечено на ' + correctAnswers + '.</div > ';
}