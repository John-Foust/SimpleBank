USE simplebank

CREATE TABLE bank(
   bank_name VARCHAR (50)     NOT NULL,
   bank_id int NOT NULL identity,
   PRIMARY KEY (bank_id)
);

CREATE TABLE ACC_Owner(
   ACC_Owner_ID   INT  NOT NULL identity,    
   owner_name varchar(50) NOT NULL,
   social varchar(10) NOT NULL, 
   username varchar(20) NOT NULL,
   passwowrd varchar(20) NOT NULL,
   PRIMARY KEY(ACC_Owner_ID), 
);

CREATE TABLE ACC(
   ACC_ID int NOT NULL identity,
   bank_ID INT      NOT NULL,
   owner_ID INT      NOT NULL,
   ACC_Type  INT              NOT NULL,
   balance decimal NOT NULL,
   PRIMARY KEY(ACC_ID),
   FOREIGN KEY (bank_ID) REFERENCES bank(bank_ID),
   FOREIGN KEY (owner_ID) REFERENCES ACC_Owner(ACC_Owner_ID )
);

CREATE TABLE transactions(
   ACC_ID   INT              NOT NULL,
   bank_ID INT      NOT NULL,
   trans_ID INT NOT NULL identity,
   trans_type INT NOT NULL,
   trans_amount decimal NOT NULL,
   trans_date DATETIME NOT NULL,
   PRIMARY KEY(trans_ID),
   FOREIGN KEY (ACC_ID) REFERENCES ACC(ACC_ID)
);