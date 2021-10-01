import axios from "axios";
import React, {useEffect} from "react";

class Dashboard extends React.Component{

    useEffect(){
        axios.get()
    }
    render(){
        return(
            <div>
                <ul class="list-group">
                    <li class="list-group-item">Profile Type: Tutor</li>
                    <li class="list-group-item">Name: Devendra</li>
                    <li class="list-group-item">Welcome to Dashboard</li>
                </ul>
            </div>
        )
    }
}

export default Dashboard