import { useEffect, useState } from 'react';
import api from '../../services/api';
import { AutoComplete } from 'primereact/autocomplete';

const initialPermissionInfo = {
    employeeForename: '',
    employeeSurname: '',
    permissionTypeId: ''
};

function EditPermission({ Id, setPermissionEdited }) {
    const [permissionInfo, setPermissionInfo] = useState(initialPermissionInfo);
    const [permissionTypes, setPermissionTypes] = useState([]);
    const [filteredPermissionTypes, setFilteredPermissionTypes] = useState([]);
    const [permissionTypeId, setPermissionTypeId] = useState('');

    useEffect(() => {
        fetchPermissionData();
        fetchPermissionTypes();
    }, []);

    const fetchPermissionData = async () => {
        try {
            const response = await api.get('/permissions/' + Id);
            if (response) {
                const permission = response.data;
                setPermissionInfo({
                    employeeForename: permission.employeeForename,
                    employeeSurname: permission.employeeSurname,
                    permissionTypeId: permission.permissionType?.id || ''
                });
                setPermissionTypeId(permission.permissionType?.name || '');
            }
        } catch (e) {
            console.log(e);
        }
    };

    const fetchPermissionTypes = async () => {
        try {
            const response = await api.get('/permissiontypes');
            if (response) {
                setPermissionTypes(response.data);
            }
        } catch (error) {
            console.error('Error fetching permission types:', error);
        }
    };

    const search = (event) => {
        const keyword = event.query.toLowerCase();
        const filtered = permissionTypes.filter((type) =>
            type.name.toLowerCase().includes(keyword)
        );
        setFilteredPermissionTypes(filtered);
    };

    const editExistPermission = async () => {
        try {
            const response = await api.put('/permissions/' + Id, permissionInfo);
            if (response) {
                setPermissionEdited();
            }
        } catch (e) {
            console.log(e);
        }
    };

    return (
        <div className='permission-view _edit-view' style={{ margin: '20px' }}>
            <div className='box sm-3'>
                <div className='row'>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>First Name:</span>
                            <input
                                type='text'
                                className='form-control'
                                placeholder='Enter First Name'
                                value={permissionInfo.employeeForename}
                                onChange={e => setPermissionInfo({ ...permissionInfo, employeeForename: e.target.value })}
                                style={{ margin: '10px' }}
                            />
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Last Name:</span>
                            <input
                                type='text'
                                className='form-control'
                                placeholder='Enter Last Name'
                                value={permissionInfo.employeeSurname}
                                onChange={e => setPermissionInfo({ ...permissionInfo, employeeSurname: e.target.value })}
                                style={{ margin: '10px' }}
                            />
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>Permission Type:</span>
                            <AutoComplete
                                value={permissionTypeId}
                                suggestions={filteredPermissionTypes}
                                completeMethod={search}
                                field="name"
                                onChange={(e) => {
                                    const selectedType = e.value;
                                    setPermissionTypeId(selectedType.name);
                                    setPermissionInfo({ ...permissionInfo, permissionTypeId: selectedType.id });
                                }}
                                forceSelection
                                style={{ margin: '10px', width: '100%' }}
                            />
                        </p>
                    </div>
                </div>
            </div>
            <button className='btn btn-success' onClick={editExistPermission} style={{ margin: '10px' }}>Submit</button>
        </div>
    );
}

export default EditPermission;
