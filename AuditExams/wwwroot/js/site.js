// Write your JavaScript code.
$('#sendBtn').click(function () {

    let tempData = {};
    let toSend = [];
    let i = 1
    let forms = $('fieldset');
    forms.each(function () {
        tempData['QuestionId'] = $(this).find('#questionId').val();
        tempData['AnswerNum'] = $(this).find('input:checked').val();
        toSend.push(tempData);
        tempData = {};
    });

    console.log(JSON.stringify(toSend));
 

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
    console.log('Пришло с сервера ' + data); //data - данные с сервера
}

function errorFunc(errorData) {
    console.log(errorData.responseText);
}
