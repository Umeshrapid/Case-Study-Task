import React, { useState } from "react";
import axios from 'axios';
import {useHistory} from 'react-router-dom';

function Registration() {
  const [userName, setUserName] = useState('')
  const [email, setemail] = useState('')
  const [password, setpassword] = useState('')
  const [role, setRole] = useState('')
  const history = useHistory();

  const handleSubmit = (event) => {
     event.preventDefault();
     axios.post('http://localhost:59935/api/Account/register', {
      userName,
      email,
      password,
      role
     }).then(response => {

      history.push('/login')
     })
  }
  return (
    <div class="container-fluid vh-100" style={{ marginTop: "100px" }}>
      <div class="" style={{ marginTop: "200px" }}>
        <div class="rounded d-flex justify-content-center">
          <div class="col-md-4 col-sm-12 shadow-lg p-5 bg-light">
            <div class="text-center">
              <h3 class="text-primary">Create Account</h3>
            </div>
            <div class="p-4">
              <form onSubmit={handleSubmit}>
                <div class="input-group mb-3">
                  <span class="input-group-text bg-primary">
                    <i class="bi bi-person-plus-fill text-white"></i>
                  </span>
                  <input
                    type="text"
                    class="form-control"
                    placeholder="Username"
                    name= "username"
                    onChange={e => setUserName(e.target.value)}
                  />
                </div>
                <div class="input-group mb-3">
                  <span class="input-group-text bg-primary">
                    <i class="bi bi-envelope text-white"></i>
                  </span>
                  <input
                    type="email"
                    class="form-control"
                    placeholder="Email"
                    name= "email"
                    onChange={e => setemail(e.target.value)}
                  />
                </div>
                <div class="input-group mb-3">
                  <span class="input-group-text bg-primary">
                    <i class="bi bi-key-fill text-white"></i>
                  </span>
                  <input
                    type="password"
                    class="form-control"
                    placeholder="password"
                    name="password"
                    onChange={e => setpassword(e.target.value)}
                  />
                </div>
                <div class="input-group mb-3">
                  <span class="input-group-text bg-primary">
                    <i class="bi bi-key-fill text-white"></i>
                  </span>
                  <div class="dropdown" style={{backgroundColor: "#fff"}}>
                    <button
                      class="btn dropdown-toggle"
                      style={{border: "1px solid #ced4da"}}
                      type="button"
                      id="dropdownMenuButton1"
                      data-bs-toggle="dropdown"
                      aria-expanded="false"
                    >
                      - Select -
                    </button>
                    <ul
                      class="dropdown-menu"
                      aria-labelledby="dropdownMenuButton1"
                    >
                      <li>
                        <a class="dropdown-item" name='tutor' onClick={(e)=>setRole(e.target.name)}href="#">
                          Tutor
                        </a>
                      </li>
                      <li>
                        <a class="dropdown-item" name='student' onClick={(e)=>setRole(e.target.name)} href="#">
                          Student
                        </a>
                      </li>
                    </ul>
                  </div>
                </div>
                <div class="d-grid col-12 mx-auto">
                  <button class="btn btn-primary" type="button">
                    <span></span> Sign up
                  </button>
                </div>
                <p class="text-center mt-3">
                  Already have an account?
                  <a class="text-primary" href="/login">
                    Sign in
                  </a>
                </p>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Registration;
