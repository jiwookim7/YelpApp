-- Calculate and update the “numCheckins”, “numTips”, “totalLikes”, and “tipCount” attributes for 
-- each business. 

-- “numCheckins” of business
CREATE TABLE TempBusinessCheckins
(
	business_id VARCHAR,
	numCheckins INT DEFAULT 0 NOT NULL,
	PRIMARY KEY (business_id)
);

INSERT INTO TempBusinessCheckins (business_id, numCheckins)
	(SELECT Business.business_id, count(*) as numCheckins
       FROM Business, Checkins
      WHERE Business.business_id = Checkins.business_id
   GROUP BY Business.business_id);

UPDATE Business
SET numCheckins = 
    (SELECT coalesce(max(numCheckins), 0)
       FROM TempBusinessCheckins
      WHERE Business.business_id = TempBusinessCheckins.business_id);


-- “numTips” of business
CREATE TABLE TempBusinessTips
(
	business_id VARCHAR,
	numTips INT DEFAULT 0 NOT NULL,
	PRIMARY KEY (business_id)
);

INSERT INTO TempBusinessTips (business_id, numTips)
	(SELECT Business.business_id, count(*) as numTips
       FROM Business, Tip
      WHERE Business.business_id = Tip.business_id
   GROUP BY Business.business_id);

UPDATE Business
SET numTips = 
    (SELECT coalesce(max(numTips), 0)
       FROM TempBusinessTips
      WHERE Business.business_id = TempBusinessTips.business_id);

-- “totalLikes” & “tipCount” of user id
CREATE TABLE TempUserTipLikes
(
	user_id VARCHAR,
	numTips INT DEFAULT 0 NOT NULL,
	totalLikes INT DEFAULT 0 NOT NULL,
	PRIMARY KEY (user_id)
);

INSERT INTO TempUserTipLikes (user_id, numTips, totalLikes)
	(SELECT Users.user_id, COUNT(*) AS numTips, SUM(likes) AS totalLikes
       FROM Users, Tip
      WHERE Users.user_id = Tip.user_id
   GROUP BY Users.user_id);
   
UPDATE Users
SET tipcount = 
    (SELECT coalesce(max(numTips), 0)
       FROM TempUserTipLikes
      WHERE Users.user_id = TempUserTipLikes.user_id),
	 totallikes = 
    (SELECT coalesce(max(totalLikes), 0)
       FROM TempUserTipLikes
      WHERE Users.user_id = TempUserTipLikes.user_id);