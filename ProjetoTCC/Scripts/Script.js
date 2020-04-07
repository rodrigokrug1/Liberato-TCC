//### Calcula a data de nascimento e devolve no campo ###
function getAge(DtNascimento) {
    const current = new Date()
    let diff = current.getFullYear() - DtNascimento.getFullYear()

    if (
        new Date(current.getFullYear(), current.getMonth(), current.getDate()) <
        new Date(current.getFullYear(), DtNascimento.getMonth(), DtNascimento.getDate())
    )
        diff--

    return diff
}
// "Pegar" os elementos dos campos:
const birthField = document.querySelector('#DtNascimento')
const ageField = document.querySelector('#idade')

// Calcular a idade sempre que o campo da data de nascimento for alterado:
birthField.addEventListener('change', (event) => {
    const date = new Date(event.target.value)

    ageField.value = getAge(date) + ' anos'
})

/*
function Idade() {
    hoje = new Date;
    nascimento = new Date($("#dtnascimento").val());
    var diferencaAnos = hoje.getFullYear() - nascimento.getFullYear();
    if (new Date(hoje.getFullYear(), hoje.getMonth(), hoje.getDate()) <
        new Date(hoje.getFullYear(), nascimento.getMonth(), nascimento.getDate()))
        diferencaAnos--;
    alert(diferencaAnos);
}  
*/