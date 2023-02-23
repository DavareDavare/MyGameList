const express = require('express');
const mongoose = require('mongoose');
const routes = require('./routes.js')
const bodyParser = require('body-parser');

const app = express();

app.use(bodyParser.json());
app.use('/', routes);

mongoose.connect('mongodb://localhost/gamedetails', {
  useNewUrlParser: true,
  useUnifiedTopology: true
});

const PORT = 5000;

app.listen(PORT, () => {
  console.log(`Server started on port ${PORT}`);
});