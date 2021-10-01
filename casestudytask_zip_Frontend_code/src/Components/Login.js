import React, {useState} from 'react';
import {useHistory} from 'react-router-dom';
import axios from 'axios';

function Login (){
  const [email, setemail] = useState('')
  const [password, setpassword] = useState('')
  const history = useHistory();

  const handleSubmit = (event) => {
    event.preventDefault();
    axios.post('http://localhost:59935/api/Account/SignIn', {
     email,
     password
    }).then(response => {
      localStorage.setItem('userDetail',response.data)
     history.push('/dashboard')
    })
 }
 console.log(email, password)
  return(
    <div class="container-fluid vh-100" style={{marginTop:"100px"}}>
    <div class="" style={{marginTop:"200px"}}>
        <div class="rounded d-flex justify-content-center">
            <div class="col-md-4 col-sm-12 shadow-lg p-5 bg-light">
                <div class="text-center">
                    <h3 class="text-primary">Login Your Account</h3>
                </div>
                <div class="p-4">
                    <form action="">
                        <div class="input-group mb-3">
                            <span class="input-group-text bg-primary"><i
                                    class="bi bi-envelope text-white"></i></span>
                            <input type="email" class="form-control" onChange={e => setemail(e.target.value)} placeholder="Email"/>
                        </div>
                        <div class="input-group mb-3">
                            <span class="input-group-text bg-primary"><i
                                    class="bi bi-key-fill text-white"></i></span>
                            <input type="password" class="form-control" onChange={e => setpassword(e.target.value)} placeholder="password"/>
                        </div>
                        <div class="d-grid col-12 mx-auto" style={{paddingBottom: "10px"}}>
                            <button class="btn btn-primary" onClick={handleSubmit} type="button"><span></span> Sign In</button>
                        </div>
                        <a href='/registration'>Create An Account</a>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>
  )
}

export default Login;