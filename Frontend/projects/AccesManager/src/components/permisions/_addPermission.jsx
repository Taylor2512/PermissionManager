import { useEffect, useState } from 'react'
import api from '../../services/api';
import { AutoComplete } from 'primereact/autocomplete';        
const initialPermissionInfo = {
    FirstName: '',
    LastName: '',
    PermissionTypeId: '',
}

function AddPermission(props) {
    const [permissionInfo, setPermissionInfo] = useState(initialPermissionInfo);
    const [permissionTypes, setPermissionTypes] = useState([]);
    const [filteredPermissionTypes, setFilteredPermissionTypes] = useState([]);
    const [permissionTypeId, setPermissionTypeId] = useState('');

    useEffect(() => {
        api.get('/permissiontypes')
            .then(response => {
                setPermissionTypes(response.data);
            })
            .catch(error => {
                console.error('Error fetching permission types:', error);
            });
    }, []);

    const search = (event) => {
        const keyword = event.query.toLowerCase();
        const filtered = permissionTypes.filter((type) =>
            type.name.toLowerCase().includes(keyword)
        );
        setFilteredPermissionTypes(filtered);
    }

    const addNewPermission = async () => {
        try {
            const response = await api.post('/permissions', permissionInfo);
            if (response) {
                props.setPermissionAdded(); // Esta función cerrará el diálogo y actualizará la lista de permisos.
            }
        } catch (e) {
            console.log(e)
        }
    }
    

    return (
        <div className='permission-view _add-view' style={{ margin: '20px' }}>
            <div className='box sm-3'>
                <div className='row'>
                    <div className='col-sm-12 col-md-6'>
                        <p>
                            <span>First Name:</span>
                            <input
                                type='text'
                                className='form-control'
                                placeholder='Enter Full Name'
                                value={permissionInfo.FirstName}
                                onChange={e => setPermissionInfo({ ...permissionInfo, FirstName: e.target.value })}
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
                                placeholder='Enter Full Name'
                                value={permissionInfo.LastName}
                                onChange={e => setPermissionInfo({ ...permissionInfo, LastName: e.target.value })}
                                style={{ margin: '10px' }}
                            />
                        </p>
                    </div>
                    <div className='col-sm-12 col-md-3'>
                        <p>
                            <span>Permission Type:</span>
                            <AutoComplete
                                value={permissionTypeId}
                                suggestions={filteredPermissionTypes}
                                completeMethod={search}
                               
                                field="name" // Campo del objeto que será mostrado
                                onChange={(e) => {
                                    setPermissionTypeId(e.value);
                                    setPermissionInfo({ ...permissionInfo, PermissionTypeId: e.value.id });
                                }}
                                forceSelection
                                style={{ margin: '10px', width: '100%' }}
                            />
                        </p>
                    </div>
                </div>
            </div>
            <button className='btn btn-success' onClick={() => addNewPermission()} style={{ margin: '10px' }}>submit</button>
        </div>
    )
}

export default AddPermission;
