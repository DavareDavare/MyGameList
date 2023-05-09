const mongoose = require('mongoose');

//Vorlage wie die einzelnen "User" in der Datenbank ausschauen sollen
const mySchema = new mongoose.Schema({
  _id: { type: String, required: true },
  gameids: { type: [Number], required: true}
},
{
  //unnötiger Datenbank Eintrag wird hier deaktiviert
    versionKey: false,
});

//Gibt an was für ein Model zu benutzen ist
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