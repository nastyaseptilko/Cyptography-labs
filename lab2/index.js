const fs = require('fs');

let firstTask = fs.readFileSync('./text1.txt');
let firstTaskAngl = fs.readFileSync('./text2.txt');
const secondTask = fs.readFileSync('./text3.txt');
const thirdTask = fs.readFileSync('./text5.txt');
const regExpSimbols = /[ |0-9|,|;|:|\/|'|.|~|"|(|)|=|-|—|^|?|*|&|%|$|#|!|@|+|\||<|>|\\|\r|\n|\t]/g;

const alfabhetRUS = 'абвгдеёжзийклмнопрстуфхцчшщъыьэюя';
const FIO = 'анстеплкияов';
const alfabhetAngl = 'abcdefghijklmnopqrstuvwxyz'
const binAlf = '01';

//Метод replace() возвращает новую строку с некоторыми или всеми сопоставлениями с шаблоном, заменёнными на заменитель.
//Метод toLowerCase() возвращает значение строки, на которой он был вызван, преобразованное в нижний регистр.
firstTask = firstTask.toString().toLowerCase().replace(regExpSimbols, '');
firstTaskAngl = firstTaskAngl.toString().toLowerCase().replace(regExpSimbols, '');

let hartley = n => Math.log2(n);

let shanon = (str, alfabhet) => {
  let H = 0;
  for(let i = 0; i < alfabhet.length; i++) {    
    let symbol = alfabhet.charAt(i), 
        s = new RegExp(symbol, 'g'), //Если у регулярного выражения есть флаг g, то он возвращает массив всех совпадений, без скобочных групп и других деталей.
        //опускает все RegExp
        p = (str.match(s) === null) ? 0 : str.match(s).length / str.length; //проверяем то что в () ?если тру ,то 0, если нет то дальше выполняется
    
    console.log(`символ: '${symbol}', p(${symbol}) = ${p}`);
    if(p !== 0) {
      H += p * Math.log2(p);
    } 
  }
  return -H;
}

//3
let shanonByName = (name, alfabhet) => name.length * shanon(name, alfabhet);
let hartleyByName = (name, alfabhet) => name.length * hartley(alfabhet.length);

//4 задание
let lastTask = someNumber => {
  const p = someNumber;
  const q = 1 - p;
  const h = (- p * Math.log2(p) - q * Math.log2(q)) || 0;
  return (1 - h);
};

const str = 'Септилко Анастасия Антоновна';
const str1 = str.toLowerCase().replace(regExpSimbols, '');

console.log(`**********Задание 1**********`)
console.log(`Длина текста = ${firstTask.length}`);
console.log(`Энтропия по Шеннону russian: ${shanon(firstTask, alfabhetRUS)}`);
console.log(`Энтропия по Хартли russian: ${hartley(alfabhetRUS.length)}`);
console.log(`Энтропия по Шеннону english: ${shanon(firstTaskAngl, alfabhetAngl)}`);
console.log(`Энтропия по Хартли english: ${hartley(alfabhetAngl.length)}`);

console.log(`**********Задание 2**********`)
console.log('Энтропия бинарного алфавита:', shanon(secondTask.toString(), binAlf));

console.log(`**********Задание 3**********`)
console.log(`Количество информации(по Шеннону): ${shanonByName(str1, FIO)}`);
console.log(`Количество информации(по Хартли): ${hartleyByName(str1, FIO)}`);
console.log(`Количество информации(в бинарном виде): ${shanonByName(thirdTask.toString(), binAlf)}`);

console.log(`**********Задание 4**********`)
console.log("ФИО при 0,1 вероятности ошибочной передачи единичного бита сообщения:", lastTask(0.1) * str1.length)
console.log("ФИО при 0,5 вероятности ошибочной передачи единичного бита сообщения:", lastTask(0.5) * str1.length)
console.log("ФИО при 1 вероятности ошибочной передачи единичного бита сообщения:",   lastTask(1.0) * str1.length)
