const express = require('express');
const Game = require('./models/gameModel.js');
const mongoose = require('mongoose');
const app = express();

    //holt alle Games auf einmal und gibt diese aus
app.get('/getAll', async (req, res) => {
  try
  {
    const myGames = await Game.find();
    res.send(myGames);
  }
  catch(error)
  {
    res.status(error);
  }
  
});

    //holt Game per ID und gibt dieses zurück
app.get('/get/:id', async (req, res) => {
  //holt ID der URL
  const id = req.params.id;
  try
  {
    const document = await Game.findById(id);
    //Wenn es die ID nicht gibt
    if (!document) {
      res.send("Not Found");
      return;
    }

    // Wenn es die ID gibt
    res.status(document);

  } catch (error) {
    res.status("Couldnt Get");
  }
})
    //löscht Game per ID
app.delete('/delete/:id', async (req, res) => {
  //holt ID der URL
  const id = req.params.id;
  try
  {
    //Wenn es ID gibt wird der Eintrag gelöscht
    const deleted = await Game.findByIdAndDelete(id);

    // Wenn es ID nicht gibt
    if (!deleted) {
      res.send("Not Found");
      return;
    }

    res.send("Document Deleted");

  } catch (error) {
    // Exception Handling
    res.send("Couldnt Delete");
  }
});

    //Löscht alle Documents der Datenbank und setzt die ID vergebung zurück
app.delete('/deleteAll', async (req, res) => {
    await Game.deleteMany();
    // Löscht "counters" collection / dafür da um die ID zurückzusetzen
    mongoose.connection.db.dropCollection('counters');
    res.send("Deleted Successfully");
  });

    //fordert einige Argumente um ein neues Spiel der Datenbank hinzuzufügen
app.post('/add', async (req, res) => {
  const data = req.body;
  try
  {
  //Schaut ob es die Kombination von Spielenamen und Konsole schon gibt, wenn es die Kombination gibt wird in "docs" die id des Spieles eingetragen, wenn das Spiel zum ersten mal erscheint steht in "docs" null
  Game.exists({gamename: data.gamename, console: data.console}, function (err, docs){
    //Wenn das Spiel auf dieser Konsole noch nicht eingetragen ist, fügt es hinzu
    if(docs == null)
    {
      const newGame = Game.create({gamename: data.gamename, developer: data.developer, publisher: data.publisher, console: data.console, description: data.description, pegi: data.pegi, imagelink: data.imagelink});
      res.send("Success");
    }
    else
    {
      res.send("Game already Exists");
    }
  })

  }
  catch(error)
  {
    // Exception Handling
    res.status("Couldnt Add");
  }

});

    //Nimmt in der URL eine ID und Argumente an um den Eintrag mit der gewissen ID zu updaten 
app.put('/update/:id', async (req, res) => {
  const id = req.params.id;
  const update = req.body;

  try {
    // Sucht nach dem Spiel per ID und ändert die Einträge die in "update" stehen
    // {new: true} ist dafür da, um das neue Document zurückzugeben und nicht das alte
    const updatedDocument = await Game.findByIdAndUpdate(id, update, { new: true });
    res.send(updatedDocument);
  } catch (error) {
    //Exception Handling
    res.send("Couldnt Put");
  }
});

module.exports = app;