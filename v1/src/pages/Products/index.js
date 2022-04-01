import React, { Component, useEffect, useState } from "react";
import { ReactSearchAutocomplete } from 'react-search-autocomplete';
import { Grid, Box, Flex, Button, Badge, Select } from "@chakra-ui/react";
import { useInfiniteQuery } from "react-query";
import { fetchProductList } from "../../api";
import Card from "../../components/Card";
import { useHistory } from "react-router-dom";
import * as categoryActions from "../../redux/actions/categoryActions";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import LeftSide from "../../components/LeftSide";
function Products() {
	const [value, setValue] = useState('');
	const history = useHistory();
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




	// const currentCategory=()=>{
	// 	this.setState({current : CategoryList});
	// }

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
		<LeftSide />
		<h3>
			<Badge color="warning">Products</Badge>
			<Badge color="success">
				<Select placeholder='Select option'>
					<option value='1'> 1</option>
					<option value='1'> 2</option>
				</Select>
			</Badge>
		</h3>
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
function mapStateToProps(state) {
	return {
		// currentCategory: state.changeCategoryReducer,
		categories: state.categoryReducer
	};
}

function mapDispatchToProps(dispatch) {
	return {
		actions: {
			getCategories: bindActionCreators(
				categoryActions.getCategories,
				dispatch
			),
			//   changeCategory: bindActionCreators(
			//     categoryActions.changeCategory,
			//     dispatch
			//   ),
			//   getProducts: bindActionCreators(productActions.getProducts, dispatch)
		}   // 
	};
}
const selectCategory = category => {
	this.props.actions.changeCategory(category);
	this.props.actions.getProducts(category.id)
};


export default connect(
	mapStateToProps,
	mapDispatchToProps
)(Products);