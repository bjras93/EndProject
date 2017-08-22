var api = 'http://' + location.host + '/api/'
var statistics = {
    init: function () {
        var mood = $('#mood');
        $.ajax({
            method: 'GET',
            url: api + 'StatisticsApi/GetMood',
            success: function (result) {
                var happy = 0,
                    neutral = 0,
                    sad = 0;
               console.log(result)
                for (var i = 0; i < result.length; i++)
                {
                    var r = result[i];
                    console.log(r.Type)
                    if(r.Type == 1)
                    {
                        neutral++;
                    }
                    if (r.Type == 2) {
                        happy++;
                    }
                    if (r.Type == 3) {
                        sad++;
                    }
                }
                
                var data = {
                    responsive: true,
                    labels: [
                        'Happy',
                        'Neutral',
                        'Sad'
                    ],
                    datasets: [
                        {
                            data: [happy, neutral, sad],
                            backgroundColor: [
                "#5ad29b",
                "#36A2EB",
                "#FFCE56"
                            ],
                            hoverBackgroundColor: [
                                "#7cddb0",
                                "#36A2EB",
                                "#FFCE56"
                            ]
                        }
                    ]
                }
                var myPieChart = new Chart(mood, {
                    type: 'pie',
                    data: data,
                    options: {
                        responsive:true
                    }
                });
            }
        });

        var dInMonth = [];
        var weights = [];
        var points = [];
        $.ajax({
            method: 'GET',
            url: api + 'StatisticsApi/GetData',
            success: function (result) {


                    for (var d = 0; d < result.length; d++) {
                            if (result[d].Date.split('-')[1] == new Date().toISOString().split('-')[1]) {
                                var date = result[d].Date.split('-');
                                weights.push(result[d].Weight);
                                points.push(date[0] + '-' + date[1])
                            }
                    }
                var data = {
                    responsive: true,
                    labels: points,
                    datasets: [
                        {
                            data: weights,
                            label: 'Weight over time'
                        }
                    ],
                }
                var myLineChart = new Chart(weight, {
                    type: 'line',
                    data: data,
                    options: {
                        tooltips: {
                            mode: 'label'
                        },

                        hover: {
                            mode: 'dataset'
                        },
                        scales: {
                            xAxes: [{
                                display: true,
                                scaleLabel: {
                                    display: true,
                                    labelString: 'Interval'
                                }
                            }],
                            yAxes: [{
                                display: true,
                                scaleLabel: {
                                    display: true,
                                    labelString: 'kg'
                                }, ticks: {
                                    suggestedMin: 30,
                                    suggestedMax: result[result.length-1].Weight + 10
                                }
                            }]                            

                        }
                    }

                });
            }
        });

    }
}
function daysInMonth(anyDateInMonth) {
    return new Date(anyDateInMonth.getYear(),
                    anyDateInMonth.getMonth() + 1,
                    0).getDate();
}