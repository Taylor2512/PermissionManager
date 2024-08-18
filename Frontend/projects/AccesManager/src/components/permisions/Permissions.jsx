import { useState, useEffect } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';

import { Dialog } from 'primereact/dialog';
import { ConfirmDialog, confirmDialog } from 'primereact/confirmdialog';
import ViewPermission from './_viewPermission';
import AddPermission from './_addPermission';
import EditPermission from './_editPermission';
import api from '../../services/api'; 

function Permissions() {
    const [permissions, setPermissionsList] = useState([]);
    const [showViewMode, setShowViewMode] = useState(false);
    const [showAddMode, setShowAddMode] = useState(false);
    const [showEditMode, setShowEditMode] = useState(false);
    const [selectedPermissionId, setSelectedPermissionId] = useState(null);

    useEffect(() => {
        getAllPermissions();
    }, []);

    const getAllPermissions = async () => {
        try {
            const response = await api.get('/permissions');
            if (response) {
                setPermissionsList(response.data);
            }
        } catch (e) {
            console.log(e);
        }
    };

    const actionsTemplate = (rowData) => {
        return (
            <>
                <button className='btn btn-success' onClick={() => {
                    setSelectedPermissionId(rowData.id);
                    setShowViewMode(true);
                }}>
                    <i className='pi pi-eye'></i>
                </button>
                <button className='btn btn-primary' onClick={() => {
                    setSelectedPermissionId(rowData.id);
                    setShowEditMode(true);
                }}>
                    <i className='pi pi-file-edit'></i>
                </button>
                <button className='btn btn-danger' onClick={() => deletePermissionConfirm(rowData.id)}>
                    <i className='pi pi-trash'></i>
                </button>
            </>
        );
    };

    const deletePermissionConfirm = (Id) => {
        confirmDialog({
            message: 'Are you sure you want to delete this permission?',
            header: 'Confirmation',
            icon: 'pi pi-trash',
            accept: () => deletePermission(Id),
        });
    };

    const deletePermission = async (Id) => {
        try {
            const response = await api.delete('/permissions/' + Id);
            if (response) {
                getAllPermissions();
            }
        } catch (e) {
            console.log(e);
        }
    };

    return (
        <div className='permissions-page'>
            <div className='container'>
                <h1>Permission Management</h1>

                <div className='permissions-list'>
                    <div className='addNewPermission'>
                        <button className='btn btn-success' onClick={() => setShowAddMode(true)}>
                            <i className='pi pi-plus'></i> Add Permission
                        </button>
                    </div>
                    <DataTable value={permissions}>
                        <Column field="id" header="ID"></Column>
                        <Column field="employeeForename" header="First Name"></Column>
                        <Column field="employeeSurname" header="Last Name"></Column>
                        <Column field="permissionTypeName" header="Permission Name"></Column>
                        <Column header="Actions" body={actionsTemplate}></Column>
                    </DataTable>
                </div>
            </div>

            <Dialog header="View Permission Data"
                visible={showViewMode}
                style={{ width: '70vw' }}
                onHide={() => setShowViewMode(false)}>
                <ViewPermission Id={selectedPermissionId} />
            </Dialog>

            <Dialog header="Add New Permission"
                visible={showAddMode}
                style={{ width: '35vw' }}
                onHide={() => setShowAddMode(false)}>
                <AddPermission setPermissionAdded={() => {
                    setShowAddMode(false);  // Close dialog
                    getAllPermissions();    // Refresh permissions list
                }} />
            </Dialog>

            <Dialog header="Edit Existing Permission"
                visible={showEditMode}
                style={{ width: '70vw' }}
                onHide={() => setShowEditMode(false)}>
                <EditPermission Id={selectedPermissionId} setPermissionEdited={() => {
                    setShowEditMode(false);
                    getAllPermissions();
                }} />
            </Dialog>

            <ConfirmDialog />
        </div>
    );
}

export default Permissions;
