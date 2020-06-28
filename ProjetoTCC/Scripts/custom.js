let primeiroGrafico = document.getElementById('graficoLinha').getContext('2d');

let chart = new Chart(graficoLinha, {
    type: 'line',

    data: {
        labels: ['2000', '2001', '2002', '2003', '2004', '2005'],

        datasets: [
            {
                label: 'Membros por facção',
                data: [173448346, 175885229, 178276128, 180619108, 182911487, 185150806]
            }
        ]
    }
});