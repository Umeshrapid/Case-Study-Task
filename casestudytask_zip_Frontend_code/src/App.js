import React from 'react';
import './App.css';
import {Switch, Route } from 'react-router-dom';
import Dashboard from './Components/Dashbord';
import Login from './Components/Login';
import Registration from './Components/Registration';


function App() {
  let userDetail  = JSON.stringify(localStorage.getItem('userDetail'))
  
  return (
    <div className="App">
     <Switch> 
       {userDetail &&
       <Route exact path='/' component={Dashboard}></Route>
       }
      <Route exact path='/login' component={Login}></Route>
      <Route exact path='/registration' component={Registration}></Route>

     </Switch>
    </div>
  );
}
export default App;