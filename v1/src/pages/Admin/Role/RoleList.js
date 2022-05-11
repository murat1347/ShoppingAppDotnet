 
<div class="row">
    <div class="col-md-12">
        <h1 class="h3">Role List</h1>
        <a class="btn btn-primary btn-sm" href="/admin/role/create">Create Role</a>
        <table class="table table-bordered mt-3">
            <thead>
                <tr>
                    <td style="width: 250px;">Id</td>
                    <td>Role Name</td>
                    <td style="width: 160px;"></td>
                </tr>
            </thead>
            <tbody>
               if(!Model == 0){
                        Model.map((item) =>(
                        
                            <tr>
                                <td>{item.Id}</td>
                                <td>{item.Name}</td>                              
                                <td>
                                    <a href="/admin/role/@item.Id" class="btn btn-primary btn-sm mr-2">Edit</a>
                                    
                                    <form>
                                        <input type="hidden" name="RoleId" value="itemId" />
                                        <button type="submit" class="btn btn-danger btn-sm">Delete</button>
                                    </form>
                                </td>
                            </tr>
                            ),)
                        }
                 else{
                    <div class="alert alert-warning">
                        <h3>No Roles</h3>
                    </div>
                }

            
            </tbody>
        </table>
    </div>
</div>