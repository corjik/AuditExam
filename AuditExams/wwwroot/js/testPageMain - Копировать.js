//document.addEventListener('DOMContentLoaded', documentReady);
$(document).ready(function () {
    courseName = document.querySelector('h1').innerHTML;

    if (localStorage.getItem('course') !== courseName) {
        localStorage.clear();
        localStorage['course'] = courseName;

        $.ajax({
            type: "GET",
            url: "/Home/GenerateQuestionList",
            data: {
                course: courseName
            },
            success: function (data) {
                createHTML(data, true)
            },
            error: errorFunc
        });
    }
    else {
        console.log(JSON.stringify(localStorage));
        let toSend = JSON.stringify(localStorage);
        $.ajax({
            type: "GET",
            url: "/Home/ReloadData",
            data: {
                qList: toSend
            },
            success: function (data) {
                createHTML(data, false)
            },
            error: errorFunc
        });
    };


    function createHTML(data, firstTime) {
        let f = document.createElement("form");
        f.setAttribute('id', 'questionForm');
        let htmlContent = '';
        for (i = 0; i < data.length; i++) {
            htmlContent += '<fieldset class="form-group">\
            <input type="hidden" value="'+ data[i].id + '" name="QuestionId" id="questionId" />\
            <legend><h3>'+ data[i].text + '' + data[i].course + '</h3></legend>\
            <div class="form-check">\
                <label class="form-check-label">\
                    <input type="radio" class="form-check-input" name="'+ data[i].id + '" value="1" id="number" checked>' + data[i].variant1 + '\
                </label>\
            </div>\
                <div class="form-check">\
                    <label class="form-check-label">\
                        <input type="radio" class="form-check-input" name="'+ data[i].id + '" value="2" id="number">' + data[i].variant2 + '\
                </label>\
            </div>\
                    <div class="form-check disabled">\
                        <label class="form-check-label">\
                            <input type="radio" class="form-check-input" name="'+ data[i].id + '" value="3" id="number">' + data[i].variant3 + '\
                </label>\
            </div>\
                        <br />\
            </fieldset>'

            if (firstTime) {
                localStorage[data[i].id] = 1; //задаем у всех RB стартовое значение - 1
            };
        };
        htmlContent += '<button type="button" class="btn btn-primary btn-lg" id="sendBtn">Завершить</button>';
        f.innerHTML = htmlContent;
        document.querySelector('.body-content').appendChild(f);
        
        let inputList = document.querySelectorAll('input.form-check-input');
        for (i = 0; i < inputList.length; i++) {
            inputList[i].addEventListener('click', changeRadioStatus);
        }

        //Загрузка состояния RadioButton
        if (!firstTime) {
            for (i = 0; i < inputList.length; i++) {
                let makeChecked = document.querySelector('input[name="' + inputList[i].getAttribute('name') + '"][value="' + localStorage.getItem(inputList[i].getAttribute('name')) + '"]'); //.replace('AnswerNum ', '')
                makeChecked.checked = true;
            };
        };

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
                data: {result: toSend},
                success: successFunc,
                error: errorFunc
            });
        });
    };

    function errorFunc() {
        console.log('Сэр, это провал!');
    };


    //Добавление изменения checked в localstorage
    function changeRadioStatus() {
        let rName = this.getAttribute('name');
        let rValue = this.getAttribute('value');
        localStorage.setItem(rName, rValue);
    };

    function successFunc(data, status) {
        localStorage.clear();
        let toDel = document.querySelector('#questionForm');
        if (toDel.parentNode) {
            toDel.parentNode.removeChild(toDel);
        }

        loadResultHhml(data);

    };

    function loadResultHhml(data) {
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
            };
        };

        let div = document.createElement('div');
        let parentEl = document.querySelector('div.body-content');
        div.innerHTML = '<div>Правильно отвечено на ' + correctAnswers + '.</div>';
        parentEl.insertBefore(div, parentEl.firstChild)
    };


});

