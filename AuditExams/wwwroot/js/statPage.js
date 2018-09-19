$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/stats/GetStatistic",
        data: {},
        success: generatePlot,
        error: errorFunc
    });

    function errorFunc() {
        console.log('Сэр, это провал!');
    };


    function generatePlot(data) {
        console.log($('#info1'));
        let info1 = document.getElementById('info1');
        let info2 = document.getElementById('info2');
        console.log(info1);
        console.log(info2);
        info1.innerHTML = data.length;
        info2.innerHTML = data.correctNumber;
        console.log(data);
        var myChart = Highcharts.chart('container1', {
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: 0,
                plotShadow: false,
            },
            title: {
                text: 'Верные<br>ответы<br>',
                align: 'center',
                verticalAlign: 'middle',
                y: 40
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },
            plotOptions: {
                pie: {
                    dataLabels: {
                        enabled: true,
                        distance: -50,
                        style: {
                            fontWeight: 'bold',
                            color: 'white'
                        }
                    },
                    startAngle: -90,
                    endAngle: 90,
                    center: ['50%', '75%']
                }
            },
            series: [{
                type: 'pie',
                name: 'Статистика ответов',
                innerSize: '45%',
                data: [
                    ['Верно', data.correctNumber/data.length],
                    ['Неверно', data.incorrectNumber / data.length],
                ]
            }]
        });


        Highcharts.chart('container2', {
            chart: {
                type: 'bar'
            },
            title: {
                text: 'Темы тестирования'
            },
            xAxis: {
                categories: ['С1', 'С2', 'С3']
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Вопросов по теме'
                }
            },
            legend: {
                reversed: true
            },
            plotOptions: {
                series: {
                    stacking: 'normal'
                }
            },
            series: [{
                name: 'Кол-во вопросов',
                data: [data.c1, data.c2, data.c3]
            }]
        });
    };
});


// Build the chart
