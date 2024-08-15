import  { useState, useEffect } from 'react';
import api from '../../services/api';


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
        <div className="aro ckf dit">
        <div className="cct cgl">
          <div className="cfb">
            <h1 className="awe awm awv ayb">Permissions</h1>
            <p className="lb awg axz">A list of all the Permissions in your account including their name, title, email and role.</p>
          </div>
          <div className="lh cbh cbx cfc">
            <button type="button" className="lu aeb ajw arl asb avr awg awm ban bbt biv bpb bpc bpe bpl">Add Permission</button>
          </div>
        </div>
        <div className="lm ma">
          <div className="gc gl adp bzy cyq">
            <div className="lv tz asb avu ckf dit">
              <table className="tz acj aco">
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
    </div>
  </div>
</div>
    );
};

export default GetPermissions;
