import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { addProductAsync } from "../redux/Product/addProductService";
import { Button, Input, Stack, Box, Flex,Center } from "@chakra-ui/react";
import Error from "./Error";
import Loading from "./Loading";
export default function Form() {
  const status = useSelector((state) => state.addProductSlice.status);
  const dispatch = useDispatch();
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [image, setImage] = useState("");
  const [stock, setStock] = useState("");
  const [price, setPrice] = useState("");
  const [categoryId, setCategoryId] = useState("");
  const error = useSelector((state) => state.addProductSlice.error);
  //   const loading = useSelector((state) => state.todos.addTodoLoading);
  const handleSubmit = async (e) => {
    e.preventDefault();
    // if (!title) {
    //   return;
    // }
    await dispatch(addProductAsync({
      "addedDate": "2022-04-19T14:42:46.448Z",
      "modifiedDate": "2022-04-19T14:42:46.448Z",
      "addedBy": "string", name, description, image, stock, price, categoryId
    }));
    // setTitle("");

  };
  if (error) {
    return <Error message={error.message} />;
  }
  if(status==="succeeded"){
    return <div>success</div>
  }
  return (
    <><Center bg='tomato' w='100%' p={4} color='white' >Product Add</Center>
    <form onSubmit={handleSubmit}>

      <Flex align="center" width="full" justifyContent="center">
        <Box pt={10}>
          <Stack>
            <Input
              value={name}
              onChange={(e) => setName(e.target.value)}
              type="text"
              autoFocus
              placeholder="name?"
              className="new-todo" />
            <Input
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              type="text"
              autoFocus
              placeholder="desc"
              className="new-todo" />
            <Input
              value={image}
              onChange={(e) => setImage(e.target.value)}
              type="text"
              autoFocus
              placeholder="Image"
              className="new-todo" />
            <Input
              value={stock}
              onChange={(e) => setStock(e.target.value)}
              type="text"
              autoFocus
              placeholder="Stock"
              className="new-todo" />
            <Input
              value={price}
              onChange={(e) => setPrice(e.target.value)}
              type="text"
              autoFocus
              placeholder="price"
              className="new-todo" />

            <Input
              value={categoryId}
              onChange={(e) => setCategoryId(e.target.value)}
              type="text"
              autoFocus
              placeholder="categoryId"
              className="new-todo" />
            <Button type="submit">Tamam</Button></Stack></Box></Flex>
      {status === "loading" && (
        <div
          style={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
          }}
        >
          <Loading />
        </div>)}
    </form></>

  );
}