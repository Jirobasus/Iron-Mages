mergeInto(LibraryManager.library, {

  RateGame  : function () {
    ysdk.feedback.canReview()
        .then(({ value, reason }) => {
            if (value) {
                ysdk.feedback.requestReview()
                    .then(({ feedbackSent }) => {
                        console.log(feedbackSent);
                    })
            } else {
                console.log(reason)
            }
        })
  },

  ShowRestartAd  : function(){
      ysdk.adv.showFullscreenAdv({
        callbacks: {
          onClose: function(wasShown) {
            myGameInstance.SendMessage('DeathMenuManager', 'Restart');
        },
          onError: function(error) {
          myGameInstance.SendMessage('DeathMenuManager', 'Restart');
        }
    }
    });
  },

  ShowMenuAd  : function(){
      ysdk.adv.showFullscreenAdv({
        callbacks: {
          onClose: function(wasShown) {
            console.log('closed');
        },
          onError: function(error) {
            console.log('err');
        }
    }
    });
  },

  ContinueForAdExtern : function(){
    ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          myGameInstance.SendMessage('DeathMenuManager', 'ContinueForAd');
        },
        onClose: () => {
          console.log('Closed');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
  });
  },

  SaveExtern  : function(date){
      var dateString = UTF8ToString(date);
      var myObj = JSON.parse(dateString);
      player.setData(myObj);
      console.log(myObj);
  },

  LoadExtern  : function(){
    if (userInitialized){
      player.getData().then(_date => {
        const myJSON = JSON.stringify(_date);
        console.log(_date);
        myGameInstance.SendMessage('Record', 'SetPlayerInfo', myJSON);
        myGameInstance.SendMessage('SetRecord', 'SetRecordText');
      });
    }   
  },

  SetToLeaderboard  : function(wave){
    if (ysdk.isAvailableMethod('leaderboards.getLeaderboardPlayerEntry')){
      ysdk.getLeaderboards()
      .then(lb => {
        lb.setLeaderboardScore('Wave', wave);
      });
    }
  },

  LoadDevice  : function(){
    console.log('device loaded');
    console.log(isDesktop);
    if (userInitialized){
      if (isDesktop){
        myGameInstance.SendMessage('PlatformHandler', 'SetPlatformDesktop');
      }
      else {
        myGameInstance.SendMessage('PlatformHandler', 'SetPlatformMobile');
      }
    }
  },
});