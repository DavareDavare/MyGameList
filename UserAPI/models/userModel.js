const mongoose = require('mongoose');

//Vorlage wie die einzelnen "Games" in der Datenbank ausschauen sollen
const mySchema = new mongoose.Schema({
  _id: { type: String, required: true },
  gameids: { type: [Number], required: true}
},
{
  //unnötiger Datenbank Eintrag wird hier deaktiviert
    versionKey: false,
});

//Gibt an was es für ein zu benutzen ist
const User = mongoose.model('User', mySchema);

module.exports = User;


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