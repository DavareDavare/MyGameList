const mongoose = require('mongoose');
var AutoIncrement = require('mongoose-sequence')(mongoose);

const mySchema = new mongoose.Schema({
  _id: {type: Number},
  gamename: { type: String, required: true },
  developer: { type: String, required: true },
  publisher: { type: String, required: true },
  console: { type: String, required: true },
  description: { type: String, required: true },
  pegi: { type: Number, required: true },
  canmygrandmaplaythisgame: { type: Boolean, required: true }
},
{
    versionKey: false
});


mySchema.plugin(AutoIncrement, {id:'order_seq',inc_field: '_id'});

const Game = mongoose.model('Game', mySchema);

module.exports = Game;