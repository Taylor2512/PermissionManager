import { useEffect, useState } from 'react';
import api from '../../services/api';

function ViewPermission({ Id }) {
    const [permissionInfo, setPermissionInfo] = useState({
        employeeForename: '',
        employeeSurname: '',
        permissionTypeId: '',
        email: '',
        phone: '',
        website: ''
    });

    useEffect(() => {
        fetchPermissionData();
    }, []);

    const fetchPermissionData = async () => {
        try {
            const response = await api.get('/permissions/' + Id);
            if (response) {
                const permission = response.data;
                setPermissionInfo({
                    employeeForename: permission.employeeForename,
                    employeeSurname: permission.employeeSurname,
                    permissionTypeId: permission.permissionType?.name || '',
                    email: permission.email,
                    phone: permission.phone,
                    website: permission.website
                });
            }
        } catch (e) {
            console.log(e);
        }
    };

    return (
        <div className='permission-view _view-view' style={{ margin: '20px' }}>
            <div className='box sm-3'>
                <div className='row'>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>First Name:</span>
                            <span>{permissionInfo.employeeForename}</span>
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Last Name:</span>
                            <span>{permissionInfo.employeeSurname}</span>
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Permission Type:</span>
                            <span>{permissionInfo.permissionTypeId}</span>
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
        </div>
    );
}

export default ViewPermission;