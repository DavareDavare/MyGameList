const express = require('express');
const router = express.Router();
const Game = require('./models/gameModel.js');
const mongoose = require('mongoose');
const app = express();


app.get('/getAll', async (req, res) => {
  const myGames = await Game.find();
  //console.log(Object.keys(myGames).length);
  //console.log(Object.keys(myGames));
  res.send(myGames);
});

app.delete('/deleteAll', async (req, res) => {
    //Löscht alle Documents der Datenbank
    await Game.deleteMany();
    // Löscht "counters" collection / dafür da um die ID zurückzusetzen
    mongoose.connection.db.dropCollection('counters');
    res.send("Deleted Successfully");
  });

app.post('/add', async (req, res) => {
  const data = req.body;
  const myGames = await Game.find();

  //console.log(Object.keys(myGames).length);

  const newGame = await Game.create({gamename: data.gamename, developer: data.developer, publisher: data.publisher, console: data.console, description: data.description, pegi: data.pegi, canmygrandmaplaythisgame: data.canmygrandmaplaythisgame });
  //console.log(data.gamename);
  res.send("Success");

/* ----------------Wenn Spiel doppelt----------------- 
  if(Object.keys(myGames).length == 0)
  {
    const newGame = await Game.create({gamename: data.gamename, developer: data.developer, publisher: data.publisher, console: data.console, description: data.description, pegi: data.pegi, canmygrandmaplaythisgame: data.canmygrandmaplaythisgame });
    //console.log(data.gamename);
    res.send("Success");
  }
  else
  {
    for(let i=1; i < Object.keys(myGames).length; i++)
    {
        //console.log(Object.keys(myGames)[i]);
        //console.log(data.gamename);
        if(Object.keys(myGames)[i].gamename == data.gamename)
        {
            res.send("Game is already in Database")
        }
        else
        {
            //console.log("Nicht mehr Erstes")
            const newGame = await Game.create({gamename: data.gamename, developer: data.developer, publisher: data.publisher, console: data.console, description: data.description, pegi: data.pegi, canmygrandmaplaythisgame: data.canmygrandmaplaythisgame });
            //console.log(data.gamename);
            res.send("Success");
        }
    }
  }
  */

});

module.exports = app;