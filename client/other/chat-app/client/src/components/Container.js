import { useEffect } from 'react';
import { init, subscribeChat, subscribeInitialMessages } from '../socketApi';
import ChatForm from './ChatForm';

import { useChat } from '../context/ChatContext';

import ChatList from './ChatList';

function Container() {
  const { setMessages } = useChat();

  useEffect(() => {
    init();

    subscribeInitialMessages((messages) => setMessages(messages));

    subscribeChat((message) =>
      setMessages((prevMessages) => [...prevMessages, { message }])
    );
  }, []);

  return (
    <div className='App'>
      <ChatList />
      <ChatForm />
    </div>
  );
}

export default Container;
