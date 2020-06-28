  // Get the context of the canvas element we want to select
  var ctx = document.getElementById("osGraficoLinha").getContext("2d");

var graficoLinha = {
    labels: ['12:00', '13:00', '14:00', '15:00', '16:00', '17:00', '18:00'],
    datasets: [
      {
        label: ['OS abertas'],
        data: [65, 59, 80, 81, 56, 55, 40],
        fillColor: 'rgba(193, 0, 0, 0.1)',
        strokeColor: 'rgba(193, 0, 0, 1)',
        pointColor: 'rgba(193, 0, 0, 1)',
        pointStrokeColor: '#fff',
        pointHighlightFill: '#fff',
        pointHighlightStroke: 'rgba(193, 0, 0, 1)'
      },
      {
        label: ['OS encerradas'],
        data: [28, 48, 40, 19, 86, 27, 90],
        fillColor: 'rgba(151,187,205,0.2)',
        strokeColor: 'rgba(151,187,205,1)',
        pointColor: 'rgba(151,187,205,1)',
        pointStrokeColor: '#fff',
        pointHighlightFill: '#fff',
        pointHighlightStroke: 'rgba(151,187,205,1)'
      }
    ]
  };

  // Instantiate a new chart using 'data' (defined above)
  var myChart = new Chart(ctx).Line(osGraficoLinha);

