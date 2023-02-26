const express = require('express');
const mongoose = require('mongoose');
const routes = require('./routes.js')
const bodyParser = require('body-parser');
const app = express();


app.use(bodyParser.json());
// Alle Verbindungen die mit '/' beginnen, werden in die routes.js weitergeleitet --> Durch das '/' sind das alle
app.use('/', routes);

//Verbindung zur Datenbank
mongoose.connect('mongodb://localhost/gamedetails', {
  useNewUrlParser: true,
  useUnifiedTopology: true
});

//Startet Server auf Port 5000
const PORT = 5000;
app.listen(PORT, () => {
  console.log(`Server started on port ${PORT}`);
});