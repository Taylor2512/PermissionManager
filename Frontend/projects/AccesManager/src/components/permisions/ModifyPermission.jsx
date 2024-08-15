import { useState, useEffect } from 'react';
import api from '../../services/api';

const ModifyPermission = () => {
    const [permissions, setPermissions] = useState([]);
    const [selectedPermissionId, setSelectedPermissionId] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [permissionTypeId, setPermissionTypeId] = useState('');
    const [permissionDate, setPermissionDate] = useState('');
    const [permissionTypes, setPermissionTypes] = useState([]);

    useEffect(() => {
        api.get('/permissions')
            .then(response => {
                setPermissions(response.data);
            })
            .catch(error => {
                console.error('Error fetching permissions:', error);
            });

        api.get('/permissiontypes')
            .then(response => {
                setPermissionTypes(response.data);
            })
            .catch(error => {
                console.error('Error fetching permission types:', error);
            });
    }, []);

    const handleSelectChange = (e) => {
        const selectedPermission = permissions.find(permission => permission.id === parseInt(e.target.value));
        setSelectedPermissionId(selectedPermission.id);
        setFirstName(selectedPermission.firstName);
        setLastName(selectedPermission.lastName);
        setPermissionTypeId(selectedPermission.permissionTypeId);
        setPermissionDate(selectedPermission.permissionDate);
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const updatedPermission = {
            firstName,
            lastName,
            permissionTypeId: parseInt(permissionTypeId),
            permissionDate,
        };

        api.put(`/permissions/${selectedPermissionId}`, updatedPermission)
            .then(() => {
                alert('Permission modified successfully');
                // Clear form
                setSelectedPermissionId('');
                setFirstName('');
                setLastName('');
                setPermissionTypeId('');
                setPermissionDate('');
            })
            .catch(error => {
                console.error('Error modifying permission:', error);
            });
    };

    return (
        <div>
            <h2>Modify Permission</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Select Permission:</label>
                    <select value={selectedPermissionId} onChange={handleSelectChange} required>
                        <option value="">Select a permission</option>
                        {permissions.map(permission => (
                            <option key={permission.id} value={permission.id}>
                                {permission.firstName} {permission.lastName} - {permission.permissionDate}
                            </option>
                        ))}
                    </select>
                </div>
                <div>
                    <label>First Name:</label>
                    <input type="text" value={firstName} onChange={(e) => setFirstName(e.target.value)} required />
                </div>
                <div>
                    <label>Last Name:</label>
                    <input type="text" value={lastName} onChange={(e) => setLastName(e.target.value)} required />
                </div>
                <div>
                    <label>Permission Type:</label>
                    <select value={permissionTypeId} onChange={(e) => setPermissionTypeId(e.target.value)} required>
                        <option value="">Select a type</option>
                        {permissionTypes.map(type => (
                            <option key={type.id} value={type.id}>{type.description}</option>
                        ))}
                    </select>
                </div>
                <div>
                    <label>Permission Date:</label>
                    <input type="date" value={permissionDate} onChange={(e) => setPermissionDate(e.target.value)} required />
                </div>
                <button type="submit">Modify Permission</button>
            </form>
        </div>
    );
};

export default ModifyPermission;
