const nameElem = document.getElementById('myName');
const dateElem = document.getElementById('date');

const days = [
  'Pazartesi',
  'Salı',
  'Çarşamba',
  'Perşembe',
  'Cuma',
  'Cumartesi',
  'Pazar',
];

const name = prompt('Adınız Nedir', '');

function showTime() {
  const date = new Date();

  dateElem.innerText = date.toLocaleTimeString() + ' ' + days[date.getDay()];
  nameElem.innerText = name;

  setTimeout(showTime, 1000);
}

window.addEventListener('load', function () {
  showTime();
});
