//### Calcula a idade e devolve no campo ###
function getAge(DtNascimento) {
    const current = new Date();
    let diff = current.getFullYear() - DtNascimento.getFullYear();

    if (
        new Date(current.getFullYear(), current.getMonth(), current.getDate()) <
        new Date(current.getFullYear(), DtNascimento.getMonth(), DtNascimento.getDate())
    )
        diff--;

    return diff;
}
// "Pegar" os elementos dos campos:
const birthField = document.querySelector('#DtNascimento');
const ageField = document.querySelector('#idade');

// Calcular a idade sempre que o campo da data de nascimento for alterado:
$('#DtNascimento').change(function () {

    const date = new Date(event.target.value);

    ageField.value = getAge(date) + ' anos';
});

$('#idade').ready(function () {

    var data = $(this).val();
    Console.log(data);

    const date = new Date(event.target.value);

    ageField.value = getAge(date) + ' anos';
});



// ### Validação de CPF ###
$('#CPF').focusout(function () {
    var cpf = $(this).val();
    var data = {
        CPF: cpf
    }
    $.ajax({
        type: 'POST',
        url: '/Membros/ValidaCPF',
        data: data,
        error: function () { toastr.error("Não foi possível verificar o CPF")},
        success: function (data) {
            if (data !== true) {
                toastr.error("CPF inválido");
            }
        }
    });
});

// ### Validação de CNPJ ###
$('#CNPJ').focusout(function () {
    var cnpj = $(this).val();
    var data = {
        CNPJ: cnpj
    }
    $.ajax({
        type: 'POST',
        url: '/Parametros/ValidaCNPJ',
        data: data,
        error: function () { toastr.error("Não foi possível verificar o CNPJ")},
        success: function (data) {
            if (data !== true) {
                toastr.error("CNPJ inválido");
            }
        }
    });
});