import React, { useState } from "react";
import { ReactSearchAutocomplete } from 'react-search-autocomplete'
import { Grid, Box, Flex, Button } from "@chakra-ui/react";
import { useInfiniteQuery } from "react-query";
import { fetchProductList } from "../../api";
import Card from "../../components/Card";
import { useHistory } from "react-router-dom";
import {CategoryList} from ".././Category/categoryList"
function Products() {

	// const currentCategory=()=>{
	// 	this.setState({current : CategoryList});
	// }
	const [value, setValue] = useState('');
	let history = useHistory();
	const [searchValue, setSearchValue] = useState('');
	const {
		data,
		error,
		fetchNextPage,
		hasNextPage,
		isFetchingNextPage,
		status,
	} = useInfiniteQuery("product", fetchProductList, {
		getNextPageParam: (lastGroup, allGroups) => {
			const morePagesExist = lastGroup?.length === 12;

			if (!morePagesExist) {
				return;
			}

			return allGroups.length + 1;
		},
	});

	if (status === "loading") return "Loading...";

	if (status === "error") return "An error has occurred: " + error.message;
	const handleOnSearch = (string, results) => {
		// onSearch will have as the first callback parameter
		// the string searched and for the second the results.
		setValue(results.name)
		console.log(string, results)
	}

	const handleOnHover = (result) => {
		// the item hovered
		console.log(result)
	}

	const handleOnSelect = (data) => {
		//  item selected9

		history.push("/product/" + data.id)
		console.log(data)
	}

	const handleOnFocus = () => {
		console.log('Focused')
	}

	const formatResult = (item) => {
		return (
			<>
				<span style={{ display: 'block', textAlign: 'left' }}>id: {item.id}</span>
				<span style={{ display: 'block', textAlign: 'left' }}>name: {item.name}</span>
			</>
		)
	}
	const onSubmit = (e) => {
		e.preventDefault();
		setSearchValue(value);
	};

	return (<><div style={{ width: 400 }}>
		<form onSubmit={onSubmit} className='form'>
			<div className='searchBar'>
				<ReactSearchAutocomplete
					items={data.pages[0]}
					// onSearch={handleOnSearch}
					onSelect={handleOnSelect}
					// onFocus={handleOnFocus}
					autoFocus />
			</div>
			<button type='submit'>Search</button>
		</form>
	

	</div><div mt="5">
			<Grid templateColumns="repeat(3, 1fr)" gap={4}>
				{data.pages.map((group, i) => (
					<React.Fragment key={i}>
						{group.map((item) => (
							<Box w="100%" key={item.id}>
								<Card item={item} />
							</Box>
						))}
					</React.Fragment>
				))}
			</Grid>

			<Flex mt="10" justifyContent="center">
				<Button
					onClick={() => fetchNextPage()}
					isLoading={isFetchingNextPage}
					disabled={!hasNextPage || isFetchingNextPage}
				>
					{isFetchingNextPage
						? "Loading more..."
						: hasNextPage
							? "Load More"
							: "Nothing more to load"}
				</Button>
			</Flex>


		</div></>
	);
}

export default Products;
