import  { useEffect, useState } from 'react'
import api from '../../services/api';

const initialPermissionInfo = {
    id: '',
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

function EditPermission(props) {
    const [permissionInfo, setPermissionInfo] = useState(initialPermissionInfo);

    useEffect(() => {
        setPermissionInfo({ ...permissionInfo,id: props.Id})
        fetchPermissionData();
    }, []);

    const fetchPermissionData = async () => {
        try {
            const response = await api.get('/permissions/' + props.Id);
            if (response) {
                console.log(response)
                setPermissionInfo(response.data);
            }
            return
        }
        catch (e) {
            console.log(e)
        }
    }

    const editExistPermission = async () => {
        try {
            const response = await api.put('/permissions/' + props.Id, permissionInfo);
            if (response) {
                props.setPermissionEdited();
            }
        }
        catch (e) {
            console.log(e)
        }
    }


    return (
        <div className='permission-view _add-view'>
            <h1>Basic Info</h1>
            <div className='box'>
                <div className='row'>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Full Name:</span>
                            <input
                                type='text'
                                className='form-control'
                                placeholder='Enter Full Name'
                                value={permissionInfo.name}
                                onChange={e => setPermissionInfo({ ...permissionInfo, name: e.target.value })}
                            />
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Permissionname:</span>
                            <input
                                type='text'
                                className='form-control'
                                placeholder='Enter Permissionname'
                                value={permissionInfo.permissionname}
                                onChange={e => setPermissionInfo({ ...permissionInfo, permissionname: e.target.value })}
                            />
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Email Address:</span>
                            <input
                                type='text'
                                className='form-control'
                                placeholder='Enter Email Address'
                                value={permissionInfo.email}
                                onChange={e => setPermissionInfo({ ...permissionInfo, email: e.target.value })}
                            />
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Phone Number:</span>
                            <input
                                type='text'
                                className='form-control'
                                placeholder='Enter Phone Number'
                                value={permissionInfo.phone}
                                onChange={e => setPermissionInfo({ ...permissionInfo, phone: e.target.value })}
                            />
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Website:</span>
                            <input
                                type='text'
                                className='form-control'
                                placeholder='Enter Website'
                                value={permissionInfo.website}
                                onChange={e => setPermissionInfo({ ...permissionInfo, website: e.target.value })}
                            />
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
                            <input
                                type='text'
                                className='form-control'
                                placeholder='Enter City Name'
                                value={permissionInfo.address.city}
                                onChange={e => setPermissionInfo({
                                    ...permissionInfo,
                                    address: {
                                        ...permissionInfo.address,
                                        city: e.target.value
                                    }
                                })}
                            />
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Street:</span>
                            <input
                                type='text'
                                className='form-control'
                                placeholder='Enter Street Name'
                                value={permissionInfo.address.street}
                                onChange={e => setPermissionInfo({
                                    ...permissionInfo,
                                    address: {
                                        ...permissionInfo.address,
                                        street: e.target.value
                                    }
                                })}
                            />
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Suite:</span>
                            <input
                                type='text'
                                className='form-control'
                                placeholder='Enter Suite Name'
                                value={permissionInfo.address.suite}
                                onChange={e => setPermissionInfo({
                                    ...permissionInfo,
                                    address: {
                                        ...permissionInfo.address,
                                        suite: e.target.value
                                    }
                                })}
                            />
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>ZIP Code:</span>
                            <input
                                type='text'
                                className='form-control'
                                placeholder='Enter ZIP Code'
                                value={permissionInfo.address.zipcode}
                                onChange={e => setPermissionInfo({
                                    ...permissionInfo,
                                    address: {
                                        ...permissionInfo.address,
                                        zipcode: e.target.value
                                    }
                                })}
                            />
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
                            <input
                                type='text'
                                className='form-control'
                                placeholder='Enter Company Name'
                                value={permissionInfo.company.name}
                                onChange={e => setPermissionInfo({
                                    ...permissionInfo,
                                    company: {
                                        ...permissionInfo.company,
                                        name: e.target.value
                                    }
                                })}
                            />
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Catch Phrase:</span>
                            <input
                                type='text'
                                className='form-control'
                                placeholder='Enter Catch Phrase'
                                value={permissionInfo.company.catchPhrase}
                                onChange={e => setPermissionInfo({
                                    ...permissionInfo,
                                    company: {
                                        ...permissionInfo.company,
                                        catchPhrase: e.target.value
                                    }
                                })}
                            />
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>BS:</span>
                            <input
                                type='text'
                                className='form-control'
                                placeholder='Enter BS'
                                value={permissionInfo.company.bs}
                                onChange={e => setPermissionInfo({
                                    ...permissionInfo,
                                    company: {
                                        ...permissionInfo.company,
                                        bs: e.target.value
                                    }
                                })}
                            />
                        </p>
                    </div>
                </div>
            </div>

            <button className='btn btn-success' onClick={() => editExistPermission()}>Edit Permission</button>
        </div>
    )
}

export default EditPermission