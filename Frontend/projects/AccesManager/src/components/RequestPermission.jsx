import  { useState, useEffect } from 'react';
import api from '../services/api';

const RequestPermission = () => {
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [permissionTypeId, setPermissionTypeId] = useState('');
    const [permissionDate, setPermissionDate] = useState('');
    const [permissionTypes, setPermissionTypes] = useState([]);

    useEffect(() => {
        api.get('/permissiontypes')
            .then(response => {
                setPermissionTypes(response.data);
            })
            .catch(error => {
                console.error('Error fetching permission types:', error);
            });
    }, []);

    const handleSubmit = (e) => {
        e.preventDefault();
        const newPermission = {
            firstName,
            lastName,
            permissionTypeId: parseInt(permissionTypeId),
            permissionDate,
        };

        api.post('/permissions', newPermission)
            .then(() => {
                alert('Permission requested successfully');
                // Clear form
                setFirstName('');
                setLastName('');
                setPermissionTypeId('');
                setPermissionDate('');
            })
            .catch(error => {
                console.error('Error requesting permission:', error);
            });
    };

    return (
        <div>
            <h2>Request Permission</h2>
            <form onSubmit={handleSubmit}>
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
                <button type="submit">Request Permission</button>
            </form>
        </div>
    );
};

export default RequestPermission;
