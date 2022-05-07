import { useEffect, useState } from "react";
import axios from "axios";
import {
    Table,
    Thead,
    Tbody,
    Tr,
    Th,
    Td,
    TableCaption,
    TableContainer,
} from '@chakra-ui/react'
function RoleList() {
    const [data, setData] = useState([]);


    useEffect(() => {
        const fetchData = async () => {

            const response = await axios.get('http://localhost:4988/api/RoleCreate');

            setData(response.data);
        }
        fetchData();
    }, []);
    return (
        <div>{data ? (
            data.map(item => (
                <TableContainer>
                    <Table size='sm'>
                        <Thead>
                            <Tr>
                                <Th>id</Th>
                                <Th>Name</Th>
                                <Th>Action</Th>
                            </Tr>
                        </Thead>
                        <Tbody>
                            <Tr>
                                <Td>{item.id}</Td>
                                <Td>{item.name}</Td>
                                <Td>  <a href="/admin/role/@item.Id" class="btn btn-primary btn-sm mr-2">Edit</a>

                                    <form>
                                        <input type="hidden" name="RoleId" value="itemId" />
                                        <button type="submit" class="btn btn-danger btn-sm">Delete</button>
                                    </form></Td>

                            </Tr>
                        </Tbody>
                    </Table>
                </TableContainer>),
            )
        )
            : (

                <div class="alert alert-warning">
                    <h3>No Roles</h3>
                </div>
            )
        }






        </div>)
}

export default RoleList;