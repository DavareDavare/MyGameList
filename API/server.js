const express = require('express');
const mongoose = require('mongoose');
const routes = require('./routes.js')
const bodyParser = require('body-parser');
const app = express();

app.use((req, res, next) => {
  res.header('Access-Control-Allow-Origin', '*');
  res.setHeader("Access-Control-Allow-Origin", "*");
  res.setHeader("Access-Control-Allow-Credentials", "true");
  res.setHeader("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT");
  res.setHeader("Access-Control-Allow-Headers", "Access-Control-Allow-Headers, Origin,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers");
  next();
});

app.use(bodyParser.json());
// Alle Verbindungen die mit '/' beginnen, werden in die routes.js weitergeleitet --> Durch das '/' sind das alle
app.use('/', routes);



//Verbindung zur Datenbank
mongoose.connect('mongodb://127.0.0.1:27017/gamedetails', {
  useNewUrlParser: true,
  useUnifiedTopology: true
});

//Startet Server auf Port 5000
const PORT = 5000;
app.listen(PORT, () => {
  console.log(`Server started on port ${PORT}`);
});

