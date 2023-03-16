import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { Login } from "./Login";
import { Register } from "./Register";

function App() {
  return (
    <Router>
    <div className="App">
      <div className="Content">
        <Routes>
            <Route path="/">
              <index/>
            </Route>
            <Route path="/login">
              <index/>
            </Route>
            <Route path="/register">
              <index/>
            </Route>
        </Routes>
      </div>
    </div>
    </Router>
  );
}

export default App;
