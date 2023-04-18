const express = require('express');
const User = require('./models/userModel.js');
const mongoose = require('mongoose');
const app = express();

    //holt alle Users auf einmal und gibt diese aus
app.get('/getAll', async (req, res) => {
  try
  {
    const myUsers = await User.find();
    res.send(myUsers);
  }
  catch(error)
  {
    res.status(error);
  }
  
});

    //holt User per ID und gibt dieses zurück
app.get('/get/:_id', async (req, res) => {
  //holt ID der URL
  const _id = req.params._id;
  try
  {
    const document = await User.findById(_id);
    //Wenn es die ID nicht gibt
    if (!document) {
      res.send("Not Found");
      return;
    }

    // Wenn es die ID gibt
    res.send(document);
    
  } catch (error) {
    res.status("Couldnt Get");
  }
})
    //löscht User per ID
app.delete('/delete/:_id', async (req, res) => {
  //holt ID der URL
  const id = req.params._id;
  try
  {
    //Wenn es ID gibt wird der Eintrag gelöscht
    const deleted = await User.findByIdAndDelete(_id);

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
    await User.deleteMany();
    res.send("Deleted Successfully");
  });

    //fordert einige Argumente um ein neues Spiel der Datenbank hinzuzufügen
app.post('/add', async (req, res) => {
  const data = req.body;
  try
  {
  //Schaut ob es die Kombination von Spielenamen und Konsole schon gibt, wenn es die Kombination gibt wird in "docs" die id des Spieles eingetragen, wenn das Spiel zum ersten mal erscheint steht in "docs" null
  User.exists({_id: data._id}, function (err, docs){
    //Wenn das Spiel auf dieser Konsole noch nicht eingetragen ist, fügt es hinzu
    if(docs == null)
    {
      const newUser = User.create({_id: data._id, gameids: data.gameids});
      res.send("Success");
    }
    else
    {
      res.send("User already Exists");
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
app.put('/update/:_id', async (req, res) => {
  const _id = req.params._id;
  const update = req.body;

  try {
    // Sucht nach dem Spiel per ID und ändert die Einträge die in "update" stehen
    // {new: true} ist dafür da, um das neue Document zurückzugeben und nicht das alte
    const updatedDocument = await User.findByIdAndUpdate(_id, update, { new: true });
    res.send(updatedDocument);
  } catch (error) {
    //Exception Handling
    res.send("Couldnt Put");
  }
});

module.exports = app;