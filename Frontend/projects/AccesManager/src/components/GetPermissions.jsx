import  { useState, useEffect } from 'react';
import api from '../services/api';

const GetPermissions = () => {
    const [permissions, setPermissions] = useState([]);

    useEffect(() => {
        api.get('/Permissions')
            .then(response => {
                setPermissions(response.data);
            })
            .catch(error => {
                console.error('Error fetching permissions:', error);
            });
    }, []);

    return (
        <div>
            <h2>Permissions List</h2>
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Permission Type</th>
                        <th>Permission Date</th>
                    </tr>
                </thead>
                <tbody>
                    {permissions.map(permission => (
                        <tr key={permission.id}>
                            <td>{permission.id}</td>
                            <td>{permission.firstName}</td>
                            <td>{permission.lastName}</td>
                            <td>{permission.permissionTypeName}</td>
                            <td>{permission.permissionDate}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default GetPermissions;
