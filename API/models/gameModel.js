const mongoose = require('mongoose');
var AutoIncrement = require('mongoose-sequence')(mongoose);

//Vorlage wie die einzelnen "Games" in der Datenbank ausschauen sollen
const mySchema = new mongoose.Schema({
  _id: {type: Number},
  gamename: { type: String, required: true },
  developer: { type: String, required: true },
  publisher: { type: String, required: true },
  console: { type: String, required: true },
  description: { type: String, required: true },
  pegi: { type: Number, required: true },
  imagelink: { type: String, required: true },
},
{
  //unnötiger Datenbank Eintrag wird hier deaktiviert
    versionKey: false
});

//Dafür da die ID automatisch inkrementell zu vergeben
mySchema.plugin(AutoIncrement, {id:'order_seq',inc_field: '_id'});

//Gibt an was es für ein zu benutzen ist
const Game = mongoose.model('Game', mySchema);

module.exports = Game;


//--------------Postman Vorlage---------------
/* 
{
    "gamename": " ",
    "developer": " ",
    "publisher": " ",
    "console": " ",
    "description": " ",
    "pegi": 18,
    "imagelink": " "
}
*/