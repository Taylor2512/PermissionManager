import { useEffect, useState } from 'react'
import api from '../../services/api';


const initialPermissionInfo = {
    name: '',
    permissionname: '',
    email: '',
    phone: '',
    website: '',
    address: {
        city: '',
        street: '',
        suite: '',
        zipcode: ''
    },
    company: {
        name: '',
        catchPhrase: '',
        bs: ''
    }
}

function ViewPermission(props) {
    const [permissionInfo, setPermissionInfo] = useState(initialPermissionInfo);

    useEffect(() => {
        fetchPermissionData()
    }, []);

    const fetchPermissionData = async () => {
        try {
            const response = await api.get('/permissions/' + props.Id);
            if (response) {
                console.log(response.data);
                setPermissionInfo(response.data);
            }
            return
        }
        catch (e) {
            console.log(e)
        }
    }


    return (
        <div className='permission-view'>
            <h1>Basic Info</h1>
            <div className='box'>
                <div className='row'>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Full Name:</span>
                            <span>{permissionInfo.name}</span>
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Permissionname:</span>
                            <span>{permissionInfo.permissionname}</span>
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Email Address:</span>
                            <span>{permissionInfo.email}</span>
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Phone Number:</span>
                            <span>{permissionInfo.phone}</span>
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Website:</span>
                            <span>{permissionInfo.website}</span>
                        </p>
                    </div>

                </div>
            </div>

            <h1>Permission Address</h1>
            <div className='box'>
                <div className='row'>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>City:</span>
                            <span>{permissionInfo.address.city}</span>
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Street:</span>
                            <span>{permissionInfo.address.street}</span>
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Suite:</span>
                            <span>{permissionInfo.address.suite}</span>
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>ZIP Code:</span>
                            <span>{permissionInfo.address.zipcode}</span>
                        </p>
                    </div>
                </div>
            </div>

            <h1>Permission Company</h1>
            <div className='box'>
                <div className='row'>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Company Name:</span>
                            <span>{permissionInfo.company.name}</span>
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Catch Phrase:</span>
                            <span>{permissionInfo.company.catchPhrase}</span>
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>BS:</span>
                            <span>{permissionInfo.company.bs}</span>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default ViewPermission