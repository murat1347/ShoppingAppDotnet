import React from 'react';
import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import '@testing-library/jest-dom';

import SearchInput from './SearchInput';
import Header from './Header';
import EmojiResultsRow from './EmojiResultsRow';
import EmojiResultRow from './EmojiResultRow';

describe('Testing emoji-app', () => {
  test('header should be in document', () => {
    render(<Header />);
    expect(screen.getByText(/Emoji Search/i)).toBeInTheDocument();
  });

  test('emoji list should be in document', () => {
    const { queryByTestId } = render(<EmojiResultsRow />);
    expect(queryByTestId('component-emoji-result-row'));
  });

  test('check emoji filter', () => {
    render(<SearchInput />);
    const input = screen.getByLabelText('Search');
    const emojiFilt = 'Grin';
    userEvent.type(input, emojiFilt);
    expect(screen.getByText(emojiFilt)).toBeInTheDocument();
  });

  test('check copy emoji', () => {
    render(<EmojiResultRow />);
    const emoji = screen.getByText(/100/i);
    userEvent.click(emoji);
    const copyEmoji = window.ClipboardData.getData();
    expect(copyEmoji).toEqual(emoji);
  });
});
